---
layout: post
title: "Comparing bench and ambient sensor data after two months"
date: 2013-12-17
author: marcetux
tags: [electronics, raspberry-pi, arduino, sensors, data]
---
Two months of parallel bench and ambient sensor readings, and the comparison is
interesting enough to write down. The ambient sensor (DHT22 near the window) tracks
the outside temperature closely with a few hours of lag — the room is poorly insulated.
The bench sensor (ATmega-driven logger board) records the bench supply voltage and a
temperature point near the regulator, which correlates with how long I've been working
at the bench.

The temperature near the bench regulator is about 4–6°C above ambient during active
bench sessions and converges to ambient within forty minutes after I stop. The supply
voltage on the 5V rail sags about 30mV when I power the Arduino from the same supply
as the bench instruments — detectable in the data, ignorable in practice, but good to
know. The voltage logger I built the board to measure is now actually measuring something
I care about.

The analysis was a Saturday of Python in a Jupyter notebook: read both CSVs, resample
to hourly means, correlate, plot with matplotlib. Nothing statistically remarkable, but
the process of building the tool and then using it on real data to answer a real question
is satisfying in a way that building tools for imagined future data never is. The bench
logger v1.1 has earned its place. I want to add a fourth channel in v2.0 — measuring
the current draw directly using an ACS712 hall-effect sensor. That project lives in 2014.
