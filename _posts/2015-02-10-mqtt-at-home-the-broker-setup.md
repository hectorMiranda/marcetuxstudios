---
layout: post
title: "MQTT at home: setting up the broker"
date: 2015-02-10
author: marcetux
tags: [mqtt, raspberrypi, electronics, home-automation]
---
Three ESP8266 nodes are now publishing data — temperature in the living room, humidity
in the bathroom, door state on the front entrance — and routing them all through the Pi
2 running Mosquitto as a broker is working better than I expected for a weekend project.

Mosquitto's config for a home network is almost trivially short: bind to the LAN
interface, set a modest keep-alive, require a password file. The topic hierarchy I
settled on is `home/<room>/<sensor>` — so `home/living/temp` and `home/front/door` —
which lets me subscribe with wildcards: `home/living/#` for everything in the living
room, `home/+/temp` for temperature from every room. MQTT wildcards are one of those
small design decisions that pay off the moment you have more than two sensors.

What I want next is persistence and a simple dashboard. Right now Mosquitto retains the
last value of each topic in memory, which survives reconnects but not a broker restart.
I'm looking at writing the retained values to InfluxDB on receipt so I have history, and
serving a small dashboard from the Pi that plots the last 24 hours. Nothing fancy — just
a Raspberry Pi on a shelf that shows me temperature trends and whether I left the front
door open. That's the whole point of this stack.
