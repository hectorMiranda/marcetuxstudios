---
layout: post
title: "Restoring a Commodore 64 off the shelf"
date: 2021-03-08
author: marcetux
tags: [retro-computing, hardware, electronics, commodore]
---
My uncle shipped me a Commodore 64 he found in a box in his garage — breadbox
style, late-production board — and I finally had a project for the desoldering
station I've owned for two years without an excuse to use seriously. Cosmetically
sound, but the power LED flickered and BASIC was producing garbage characters on
a cold start. Classic electrolytic-cap failure on an aging board.

The SID chip era is the thing I genuinely love about restoring these machines: the
entire personality of the C64 lives in a handful of custom silicon that can't be
reproduced. The VIC-II, the SID, the two CIAs — each one a small empire of clever
1980s engineering. Cap replacement on the main board is straightforward once you
map which caps are filtering the power rails versus which are coupling audio. I
replaced the five electrolytics on the power section with modern equivalents and
the garbage-character problem vanished. Cold start: clean BASIC prompt, ready.

The cartridge port is the next project. The dead power LED turned out to be a bad
USER port transistor, not the LED itself, which I only discovered after ordering
a new LED that didn't fix anything. Lesson relearned: verify the path before
replacing the component at the end of it. The machine is running now, sitting next
to the monitor in the shop corner, and there's something quietly satisfying about
hardware that was built when I was five working again.
