---
layout: post
title: "Halloween blinkenlights"
date: 2012-10-31
author: marcetux
tags: [arduino, electronics, hardware, fun]
---

Took the night off from servers to build something useless and delightful: a
jack-o'-lantern that flickers like a real candle and snaps at you when you reach
for the candy.

Two cheap parts do the trick. An LED on a PWM pin gets a random brightness a few
times a second — turns out "flame" is mostly just *not steady*, and a little
`random(120, 255)` reads as flame far better than a constant glow. A photoresistor
watches the ambient light; when a hand casts a shadow over the pumpkin, the reading
drops below the resting level I sampled at startup, and a little servo snaps the
"jaw" shut.

The engineering lesson hiding in the candy: **calibrate against the environment,
not an absolute.** Hard-coding "it's dark when the reading is below 300" fails the
moment you move the pumpkin to a different room. Sampling ambient at boot and
detecting a *relative* drop just works, porch or living room.

Code's in `examples/2012/halloween/`. Back to the boring-on-purpose deployment
stuff next week. Happy Halloween.
