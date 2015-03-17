---
layout: post
title: "Grafana dashboard for the home sensor network"
date: 2015-03-16
author: marcetux
tags: [grafana, influxdb, raspberrypi, home-automation]
---
Grafana is running on the Pi 2 and the sensor network finally has a face. Temperature
in the living room and bathroom, humidity, front-door state, all on one dashboard with
time-range selectors. It took maybe two hours from InfluxDB-with-data to something I
actually want to look at when I walk by.

The panel configuration model is what makes Grafana click: you pick a data source
(InfluxDB in this case), write an InfluxDB query directly in the panel editor, and the
visualization updates in real time as you adjust the query. The GROUP BY time bucketing
I figured out last month feeds the line graph natively — Grafana expects time-series
data and knows what to do with it. Adding a threshold line for "temperature below 18°C
is a problem" took about 30 seconds.

The thing I'm using most is the door-state panel — a discrete state display that shows
open or closed with a green/red color. It's a single retained MQTT topic read through
InfluxDB, and I've already noticed twice that I left the door open because the dashboard
was visible from the living room couch. A silly use case but a real one. Monitoring your
own house with the same tooling you use at work turns out to be deeply satisfying —
everything is instrumented, everything has a retention policy, and the shelf Pi quietly
does its job.
