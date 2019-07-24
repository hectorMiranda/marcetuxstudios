---
layout: post
title: "Home energy monitor with InfluxDB and Grafana"
date: 2019-07-23
author: marcetux
tags: [raspberry-pi, influxdb, grafana, homelab, energy]
---
The energy monitor hardware from April is now producing data, and the data pipeline is what I've been wiring this month. The ESP32 publishes current readings over MQTT to the Pi 4, a Python bridge subscribes and writes time-series data to InfluxDB, and Grafana visualizes it. The whole stack runs on the Pi 4 and it's holding up comfortably — Pi 4's extra RAM means InfluxDB isn't constantly paging.

InfluxDB's data model — measurements, tags, and fields — maps naturally to sensor data. Each MQTT message becomes an InfluxDB point with the circuit name as a tag, and the RMS current and computed wattage as fields, at the sample timestamp. Grafana's InfluxDB data source makes building dashboards quick: a time-series panel with a flux query grouping by circuit tag gives me a stacked area chart of the apartment's power consumption over the last 24 hours.

The interesting finding: the HVAC unit is almost a third of the apartment's consumption in July, which is not surprising, but seeing it as a ratio rather than a vague sense makes me more intentional about the thermostat settings. The server rack in the home lab (the Pi cluster plus a small NAS) is running about 60W continuously, which is lower than I'd estimated. The data is working as designed — converting a vague awareness of energy use into something I can actually act on. The Grafana dashboard is becoming the most frequently-opened browser tab at home.
