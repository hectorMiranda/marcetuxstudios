---
layout: post
title: "HLS and adaptive bitrate streaming for video delivery"
date: 2016-05-05
author: marcetux
tags: [video, hls, ffmpeg, media, cdn]
---
JibJab clips are short — most under two minutes — and for a long time we served them as
progressive download MP4s. A player fetches the file, buffers ahead, plays. It works
well when the viewer's connection is fast. When it's slow, the player stalls and the
user waits. HLS adaptive bitrate streaming fixes this by letting the player pick the
rendition that matches available bandwidth rather than committing to one quality level
at playback start.

The HLS package is a set of renditions — say, 360p at 500 kbps, 540p at 1 Mbps, 720p
at 2 Mbps — plus a master playlist `.m3u8` that lists them. The player fetches small
segments (typically two to six seconds each) and re-evaluates which rendition to use
segment by segment. If bandwidth drops, the next segment comes from a lower-bitrate
rendition. The quality switch is seamless to the viewer. FFMPEG produces all of this:
`-hls_segment_size`, `-hls_playlist_type vod`, and separate passes for each rendition
or a single pass with `-map` flags.

The CDN story is simpler than I expected: segments and playlists are static files with
`Content-Type: application/vnd.apple.mpegurl` for the manifest. They cache at the edge
like any other static asset. CloudFront serves the segments from S3; the only
origin hit is the first manifest request. Latency on the first segment load went down
because short segments start playing faster than waiting for a full progressive download
to buffer. For short-form content the win is mostly robustness, not quality — but the
playback stall rate is measurably lower.
