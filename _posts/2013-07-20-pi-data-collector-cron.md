---
layout: post
title: "Turning the Pi logger into a proper data collector with cron and rotation"
date: 2013-07-20
author: marcetux
tags: [raspberry-pi, linux, python, electronics, hardware]
---
The bench logger has been writing to a single CSV file since March, which is now
several megabytes of unstructured time-series data with no rotation and no
organization. A Saturday cleanup made it into something I'd actually want to keep
running indefinitely.

The solution is boring and correct: a Python script that reads the UART and writes to a
daily file — `bench-2013-07-20.csv` — with a cron job that starts the collector at boot
and a second daily cron job that gzips previous days' files and moves them to an
archive directory. The boot cron uses `@reboot`, which I hadn't used before — it runs
once after the system starts, which is exactly the right trigger for a hardware reader
that should always be running. A `lockfile` check prevents a second instance from
starting if the cron fires and the first instance is still up for some reason.

The collector now also writes a summary line every hour: min/max/mean voltage and
temperature for the hour. The raw samples are in the daily file; the hourly summaries
are in a separate `bench-summary.csv` that doesn't rotate and stays small. When I want
to ask "was the bench supply stable last Tuesday?" I look at the hourly summary.
When I need to debug a specific event, I decompress the daily archive. The boring
infrastructure is what makes the data actually useful later.
