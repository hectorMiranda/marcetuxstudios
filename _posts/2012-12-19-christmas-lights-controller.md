---
layout: post
title: "A programmable Christmas lights controller"
date: 2012-12-19
author: marcetux
tags: [arduino, electronics, leds, fun]
---
Holiday bench project: driving a string of addressable RGB LEDs (WS2811) off an
Arduino so the Christmas lights do something better than blink on a cheap timer.

The magic of addressable LEDs is that the whole string is one data line — each pixel
reads its color off the front of the signal and passes the rest down the chain. So
"chase," "twinkle," and a slow rainbow are all just functions that write an array of
colors and push it out. The FastLED library makes it a pleasure: fill a `CRGB`
array, call `show()`, done.

The genuinely tricky part is **power, not code.** Sixty RGB pixels at full white
pull more current than the Arduino's regulator will ever give — you feed the strip
from a separate 5V supply and only share ground with the Arduino. Learned that the
honest way, with a dimming flicker and a warm voltage regulator, before I read the
datasheet like I should have. Code's in `examples/`.
