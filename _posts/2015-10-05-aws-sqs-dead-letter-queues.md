---
layout: post
title: "SQS dead-letter queues for failed render jobs"
date: 2015-10-05
author: marcetux
tags: [aws, sqs, video, devops, reliability]
---
A render job fails, SQS retries it, it fails again, and eventually the message sits in
the queue being picked up and reprocessed indefinitely while consuming worker capacity.
That's the situation before dead-letter queues. After: failed messages flow to a
separate DLQ after a configurable number of retries, and the primary queue stays clean.

The configuration is a `RedrivePolicy` on the source queue: max receive count of 3 means
the message gets three attempts, then moves to the dead-letter queue automatically. SQS
handles the tracking — every time a consumer receives a message without deleting it
(because it failed), the receive count increments. Hit the max, move to DLQ. The worker
code doesn't change; the DLQ is infrastructure configuration.

The operational value is in what you do with the DLQ. We have a CloudWatch alarm on
the DLQ depth — any message arriving there means a render job failed three times and
needs investigation. The messages sit in the DLQ long enough to be inspected: log the
message body, find the render job ID, look at the worker logs for that job. Once we've
diagnosed and fixed the issue, we can re-drive the messages back to the source queue via
a small script. Failed jobs don't disappear; they accumulate in a place where they can
be found, debugged, and replayed. That's the whole point of making failure a first-class
design concern.
