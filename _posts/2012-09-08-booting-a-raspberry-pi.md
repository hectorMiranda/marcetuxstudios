---
layout: post
title: "Booting a Raspberry Pi for the first time"
date: 2012-09-08
author: marcetux
tags: [raspberrypi, linux, electronics, hardware]
---

My Raspberry Pi finally arrived after a long wait — these things are back-ordered
everywhere — and I spent Saturday getting it on the network.

The ritual: `dd` a Raspbian image onto an SD card, plug in HDMI and a keyboard,
and watch a full Linux box boot off a $35 board the size of a credit card. The
first boot config tool (`raspi-config`) handles expanding the filesystem to fill
the card and enabling SSH, which is the only thing you really need before you yank
the keyboard and go headless.

What surprises you is how *normal* it is. It's Debian. `apt-get` works. Python is
right there. The 700 MHz ARM chip won't win races, but for a always-on box that
sips power and talks to GPIO pins, that's not the point.

My plan: a little home dashboard that polls a couple of sensors and serves a page
on the LAN. The Pi is the perfect machine to leave running in a closet and forget
about. More once I've wired something up to those GPIO headers.
