---
layout: post
title: "Building an operations dashboard in Grafana"
date: 2016-06-14
author: marcetux
tags: [grafana, monitoring, operations, devops, metrics]
---
The "how's the pipeline doing" question was being answered by CloudWatch dashboards,
which are fine and also ugly and also require clicking through the AWS console with the
correct filters in the right time zone. Grafana on the Pi 3 at home already had InfluxDB
behind it for the sensor data; I pointed the same pattern at the pipeline metrics and
now the operations view is a browser tab instead of an AWS console adventure.

The metrics feeding it are a mix: CloudWatch metrics surfaced through the CloudWatch
datasource in Grafana (SQS queue depth, worker instance count, error rate from
CloudWatch Logs Insights), and application-level metrics the workers emit directly to
InfluxDB (jobs processed per minute, p95 transcoding duration by rendition, DLQ depth).
CloudWatch is good at infrastructure; InfluxDB is good at the application-level timing
data that CloudWatch would require too much log-parsing to produce.

The dashboard that actually gets used has four panels: queue depth, active workers,
jobs-per-minute, and a histogram of transcoding duration. Everything else I tried to add
was noise. The lesson from every dashboard I've ever built: fewer panels, picked
deliberately, beat comprehensive panels that nobody reads. The on-call dashboard shows
you the four things that tell you whether it's fine or not fine. Everything else is
archival.
