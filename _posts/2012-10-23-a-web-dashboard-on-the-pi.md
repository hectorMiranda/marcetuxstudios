---
layout: post
title: "A little web dashboard on the Pi"
date: 2012-10-23
author: marcetux
tags: [raspberrypi, python, flask, electronics]
---

The sensor data has been piling up in a CSV for weeks. Time to actually *look* at
it without SSHing in and tailing a file. I put a tiny Flask app on the Pi that
serves the latest readings on the LAN.

Flask is the right size for this. A handful of lines: read the tail of the CSV,
render an HTML table, serve it on port 5000. No database, no framework ceremony —
the Pi is already the thing that "remembers," so the web layer just reads what's
there and shows it. I bind to `0.0.0.0` so any machine in the house can hit
`http://raspberrypi:5000` and see the bench temperature.

It's gloriously over-simple and that's deliberate. This is a thing on my LAN that I
look at twice a day; it does not need authentication, a build pipeline, or a
JavaScript framework. The whole appeal of the Pi is that "good enough, running in
the closet" beats "perfect, never finished."

Next iteration I'll add a little chart instead of a raw table — probably a sparkline
so I can see the daily temperature swing at a glance.

*Update: the app is in `examples/2012/pi-dashboard/app.py` — it reads the tail of
the CSV `logger.py` writes and serves it on the LAN.*
