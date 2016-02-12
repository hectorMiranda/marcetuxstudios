---
layout: post
title: "Generating video thumbnails at scale with FFMPEG"
date: 2016-02-11
author: marcetux
tags: [ffmpeg, media, aws, pipeline, video]
---
Every video upload at JibJab needs a thumbnail — several, actually, for different
contexts and aspect ratios — and for a long time the job was part of the main transcode
worker. That coupling caused problems: a thumbnail failure could block a full-resolution
rendition from completing, and thumbnail jobs swamped the queue when upload volume
spiked. Splitting them into a separate worker was the right call.

The FFMPEG command is simple enough: `-ss` to seek to a timestamp, `-vframes 1` to
extract one frame, `-vf scale=…` to resize. The interesting part is the `-ss` position
strategy. Seeking to the very start gives a black frame half the time. Seeking to
10% of the duration is better; seeking to the highest-scoring frame via `select` filter
is the right answer for quality but burns real CPU per video. We use the 10% heuristic
for the fast path and queue the scored version separately for anything that goes on a
landing page.

The split gave us independent scaling: thumbnail workers are small instances and scale
on queue depth independently of the transcode fleet. FFMPEG thumbnail is fast — a 60-
second video produces three sizes in under two seconds on a `t2.small` — so we can
pack several workers per instance. Total thumbnail cost dropped significantly. The
lesson I keep finding in media pipelines: carve at the natural seams, scale each piece
independently. One big worker does everything poorly.
