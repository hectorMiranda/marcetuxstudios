---
layout: post
title: "Video thumbnail extraction at render time"
date: 2015-09-11
author: marcetux
tags: [ffmpeg, video, aws, s3, jibjab]
---
Every rendered video at JibJab needs a thumbnail for the library view — a single frame
that represents the video well enough that you want to click it. Extracting that frame
from FFMPEG during the render pipeline is the obvious place to do it, and the specific
frame selection is less trivial than it sounds.

The naive approach is `ffmpeg -i video.mp4 -ss 00:00:01 -frames:v 1 thumb.jpg` — grab
the frame at one second. This works until the one-second frame is a transition,
a near-black frame between scenes, or a frame before any character appears in a
personalized video. We use the `-sseof` flag to seek from the end for some templates
where the title card comes last, and for others we've pre-computed the "representative
frame" timestamp that was chosen by whoever designed the template.

The pipeline integration: the render worker, after the main FFMPEG transcode completes,
runs the thumbnail extraction as a second command, uploads the thumbnail to S3 with a
predictable key derived from the render job ID, and includes the thumbnail URL in the
render completion message sent to SQS. The API receives the completion message and
atomically updates both the video URL and thumbnail URL in the database. The library
view never sees a video without a thumbnail because both are set in a single update.
Keeping derived artifacts together in one transactional step is the pattern I keep
coming back to for correctness.
