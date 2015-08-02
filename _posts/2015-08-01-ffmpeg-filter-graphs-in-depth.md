---
layout: post
title: "FFMPEG filter graphs and the compositing pipeline"
date: 2015-08-01
author: marcetux
tags: [ffmpeg, video, media, jibjab, pipeline]
---
Three weeks into the JibJab video pipeline and the FFMPEG filter graph model has gone
from "I know what this does conceptually" to "I can write one from scratch." The
personalized video flow is: original animation frames as a PNG sequence, user face
image, FFMPEG filter graph that composites the face over the character, output as H.264.
The filter graph is the part that required the most learning.

The `filtergraph` ties inputs together with named pads. A two-input composite looks
like: `[0:v][1:v]overlay=x=120:y=80[out]` — video stream from input 0, video from
input 1, overlay at coordinates, output labeled `out`. You can chain: scale the
overlay input first, then overlay, then apply a color correction filter. The stream
labels let you split a stream and send it to multiple filters, or merge multiple filter
outputs into one. It's a dataflow graph and once you read it as a graph rather than a
string, the structure is clear.

The practical piece I've found most valuable: use `-lavfi` to test filter graphs on
still images before running them against full video sequences. A 30-second test on a
single frame catches filter syntax errors and coordinate math mistakes in milliseconds.
The full render job takes minutes. Short the feedback loop wherever you can; video
processing is the one domain where a 10-second feedback cycle feels luxuriously fast.
