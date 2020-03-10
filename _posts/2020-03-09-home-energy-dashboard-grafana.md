---
layout: post
title: "Building the home energy dashboard in Grafana"
date: 2020-03-09
author: marcetux
tags: [homelab, grafana, influxdb, iot]
---
The ESP32 energy-monitoring board I built last fall has been logging to InfluxDB
through MQTT for four months, and the raw numbers sit there unread most of the
time. I finally built the Grafana dashboard I'd been procrastinating on, and now the
data is actually useful.

The setup is straightforward — Grafana pulls from InfluxDB with Flux queries, and the
panel options let you visualize the per-circuit watt readings as time-series overlays.
What I wanted was a single panel showing the whole day's baseline load, the peaks from
the compressor and the clothes dryer, and a daily total in kWh. The kWh number
requires a transformation step — integrate watts over time — which Flux handles with
`integral()`, something that would have been raw SQL gymnastics in a traditional
database.

The thing that surprised me: just seeing the numbers has changed behavior. The clothes
dryer peak is bigger than I expected; the idle baseline has a ghost load I haven't
traced yet. A sensor that logs to a file no one reads is trivia. A dashboard on the
kitchen display is a decision surface. The hardware was the easy part; the display is
what makes the data worth collecting.
