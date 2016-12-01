---
layout: post
title: "November notes"
date: 2016-11-30
author: marcetux
tags: [meta, retrospective]
---
November was deliberately slow. The Friday documentation sessions produced the pipeline
architecture doc, the SQS worker runbook, and the CDN configuration guide — three things
that were in my head and are now in the repo. The new engineer who inherits this will
at least have a map. Not a perfect map but a better one than I had when I started.

The ESP32 deep sleep work is going well; the flexible wakeup sources really are better
than the ESP8266 approach. I've got a breadboard prototype doing timer wakeup, BME280
sampling, and WiFi publish reliably. The next board revision will incorporate an ESP32
module instead of the ESP-12F. Docker retrospective was useful to write — I've been
running containers in this pipeline for ten months and the honest accounting matters
for the next job. Lambda is right for event glue; it's wrong for most other things.

December will be the last month at JibJab. The plan is a clean handoff — the code
review, the architecture walk-through with the team, the final documentation pass.
Then: figure out what's next. The marketplace/platform space keeps coming up in
conversations. There's a role at a company called SolidCommerce that handles
marketplace integrations for retailers, which is a completely different domain from
video, which is appealing.
