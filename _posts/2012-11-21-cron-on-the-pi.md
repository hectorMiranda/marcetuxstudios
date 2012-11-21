---
layout: post
title: "Letting cron run the bench"
date: 2012-11-21
author: marcetux
tags: [raspberrypi, linux, cron, electronics]
---
The Pi dashboard works, but I was still starting the logger by hand like an animal.
Time to let the machine babysit itself. `cron` is the obvious answer and a good
reminder that the boring Unix tools usually win.

Two jobs. One `@reboot` line starts the serial logger so a power blip doesn't
silently end my data collection. A second job runs nightly to roll the day's
readings into a small summary file and prune the raw log so the SD card doesn't
slowly fill over months — SD cards on a Pi are not where you want unbounded growth.

The lesson that keeps recurring with this little box: it should survive a power cut
with no human in the loop. `@reboot` plus an idempotent startup script means I can
yank the cord, plug it back in, and the bench is logging again before I've sat back
down. Resilience by way of the dumbest possible mechanism.
