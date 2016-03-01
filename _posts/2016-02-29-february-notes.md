---
layout: post
title: "February notes"
date: 2016-02-29
author: marcetux
tags: [meta, retrospective]
---
Leap day close for a leap month — appropriately unusual. The Pi 3 landed and immediately
replaced the laptop-in-the-closet; the WiFi story alone was worth the wait. The
Docker pipeline for transcoding workers is running in staging and the build artifact
problem it solves is real. CDN Vary header behavior is now something I understand
viscerally rather than abstractly, thanks to an evening of mis-cached video variants.

The thumbnail worker split was the most satisfying change — a classic case of a coupled
job making both halves worse, and the fix being exactly as simple as carving at the
seam. Vue.js is worth watching; I came away from a weekend with it understanding why
people are excited about it even if I wouldn't ship a production Angular replacement
on it today. Auto Scaling Groups are tuned and the transcoding fleet is actually elastic
now, which means I stop watching CPU graphs on weekend mornings.

March: I want to get the Docker images into a proper registry and stop sharing AMIs
between environments. The KiCad board I ordered in January should arrive. And Let's
Encrypt needs to go on something with real traffic before I can call it validated.
