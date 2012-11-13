---
layout: post
title: "The responsive images problem"
date: 2012-11-12
author: marcetux
tags: [frontend, responsive, performance, html]
---
Responsive layouts are solved enough with the grid; responsive *images* are still a
mess in 2012, and it bugs me every time. We send a 1600px hero image to a phone on a
cellular connection because the markup has no clean way to say "here are sizes, pick
one."

The hacks are all unsatisfying. JavaScript that rewrites `src` after measuring the
viewport — but it runs after the browser already started fetching. Server-side
sniffing by user-agent — fragile and a maintenance sinkhole. `background-image` with
media queries — works, but only for decorative images, not content.

There's standards-track talk about a `<picture>` element and a `srcset` attribute to
let the markup describe alternatives and let the browser choose. That's clearly the
right shape. For now I'm doing the CSS-background trick where I can and serving a
sensibly-sized compromise everywhere else, and waiting for the platform to catch up.
