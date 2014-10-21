---
layout: post
title: "Raspberry Pi as a home sensor hub"
date: 2014-10-20
author: marcetux
tags: [raspberrypi, electronics, iot, maker, hardware]
---
The three ESP8266 sensors in the apartment needed somewhere to send their data, and
"a SQLite file on the laptop" has been the answer since August, which means the data
disappears when the laptop closes. This weekend I stood up a Raspberry Pi as the
permanent data collector.

The Pi runs Raspbian and a small Sinatra application — Sinatra because it's lighter than
Rails for a local API with three routes: POST a reading, GET the latest readings for a
sensor, GET a summary for the dashboard. The sensor POST hits an endpoint with the sensor
ID and the reading; Sinatra writes it to a local SQLite3 database via the `sequel` gem.
That's the whole backend. It's on a static IP on the home network via a router DHCP
reservation so the ESP8266 sensors always know where to find it.

The Pi stays on at the power strip and the data accumulates. There are now six weeks
of temperature readings from the living room, bedroom, and door sensor, plus on/off
events from the door sensor's GPIO interrupt. The visualization is a gnuplot graph
I regenerate with a cron job every five minutes and drop in a folder the Pi serves
over HTTP. Not pretty. Accurate. Eventually I'll replace gnuplot with something interactive,
but the data collection was the hard part and that's done.
