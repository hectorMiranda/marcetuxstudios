---
layout: post
title: "Adding a solar-production monitor to the home energy dashboard"
date: 2020-07-13
author: marcetux
tags: [homelab, esp32, iot, electronics]
---
The home energy story has been consumption-only: I know what I'm using but not what
the panels are producing. The inverter has a serial interface — a TTL UART output
that spits a status packet every five seconds — and an ESP32 with a level shifter
is all it takes to tap it. I've been meaning to do this since the dashboard went up
in March; July's lab project finally got it done.

The packet format is a fixed-width ASCII string with voltage, current, power output,
and daily kilowatt-hour total. The ESP32 firmware reads the UART, parses the fields,
and publishes them as MQTT topics to the broker on the k3s cluster. Node-RED does the
light pre-processing — outlier filtering, unit conversion — and writes into InfluxDB.
Grafana picks it up like any other metric. The whole path from panel production to
dashboard number is about four hundred milliseconds.

The panel now shows consumption and production on the same time axis with a calculated
net line: positive when we're importing from the grid, negative when we're exporting.
The summer afternoon peak production windows are obvious. The next step is an alert
when net consumption has been positive for more than two hours during peak sun hours —
which probably means a panel is underperforming — but that's next month's firmware.
The dashboard went from "data I can act on" to "data I didn't know I was missing."
