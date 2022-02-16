---
layout: post
title: "Adding a whole-home energy monitor to the home lab"
date: 2022-02-15
author: marcetux
tags: [electronics, raspberry-pi, energy, homelab, iot]
---
The Pi home lab has been measuring temperature, humidity, and CO2 for a while. This
month I added actual electricity monitoring — a CT (current transformer) clamp around
the main feed, feeding an ADS1115 ADC over I2C into a Pi. The goal is a dashboard
that shows real-time watts, running totals, and alerts when something's pulling more
than expected. The hardware side was an afternoon; getting the math right took longer.

A CT clamp is a non-invasive current sensor — it wraps around a wire and produces a
small AC voltage proportional to the current flowing through. The ADS1115 samples that
voltage at up to 860 times per second. From there it's signal processing: compute the
RMS of the sampled waveform over a mains cycle (16.7ms at 60Hz here), multiply by the
known mains voltage to get apparent power, apply a power factor correction if you care
about reactive loads. The honest answer is that for resistive loads (heaters, lights)
apparent power ≈ real power, and I'm not losing sleep over the reactive component yet.

The dashboard is Grafana talking to InfluxDB, same stack as the temperature sensors,
because I already had it running and consistency beats novelty. The first thing the
data showed me: the old chest freezer in the garage is cycling hard and drawing more
at startup than I expected. Replacing it would pay back in a year at current rates.
You don't know what you can't measure.
