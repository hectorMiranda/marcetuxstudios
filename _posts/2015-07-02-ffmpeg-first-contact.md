---
layout: post
title: "FFMPEG, first contact"
date: 2015-07-02
author: marcetux
tags: [ffmpeg, video, media, tooling]
---
I've been doing homework on video pipelines because the JibJab offer is real and I start
in a few weeks. FFMPEG is the tool at the center of everything they do — personalized
video rendering, format conversion, thumbnail extraction — and the honest truth is I've
never used it beyond a one-liner I copied off a forum. Time to fix that before day one.

FFMPEG's architecture is a filter graph: an input, a chain of filters that transform the
audio and video streams, and an output. The simplest invocation — `ffmpeg -i input.mp4
output.webm` — triggers a transcode using defaults. Adding filters is where it gets
interesting: `-vf scale=640:-1` scales the video to 640px wide while preserving aspect
ratio; `-vf "drawtext=text='hello':x=10:y=10"` burns text into the frame. Chain them
with commas or semicolons depending on whether streams need to split and merge.

The thing I spent most time understanding is the codec versus container distinction.
MP4 is a container; H.264 is a codec. The same container can hold different codecs; the
same codec can live in different containers. `ffmpeg -i in.mp4 -c:v libx264 -c:a aac
out.mp4` is explicit about both: H.264 video, AAC audio, MP4 container. When you leave
codec unspecified, FFMPEG picks defaults that aren't always what you want. Being explicit
from the start saves the debugging session where you transcode something and it plays on
your machine and nowhere else.
