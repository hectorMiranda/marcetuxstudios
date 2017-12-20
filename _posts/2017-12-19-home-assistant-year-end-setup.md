---
layout: post
title: "Home Assistant setup at year end"
date: 2017-12-19
author: marcetux
tags: [homeassistant, iot, esp32, raspberrypi, home]
---
A year-end inventory of the home automation setup: seven ESP32 sensors reporting
temperature and humidity from five rooms and the garage; door state sensor on the
garage; presence detection via device tracking; a dozen automations running. The Pi
cluster handles Home Assistant, Mosquitto, and a small Grafana instance that graphs
sensor history. The garage door sensor I designed in KiCad is on its second LiPo charge
and running fine.

The Grafana setup is the piece I'm happiest with. Home Assistant records sensor state
to its own SQLite database, which I replicate to a PostgreSQL instance on the Pi cluster
with a simple cron job. Grafana connects to PostgreSQL and the resulting dashboard shows
temperature trends per room over time, humidity patterns, door open/close history. The
discovery from two months of data: the bedroom runs 2-3°C warmer than the living room
from 9pm onward because of the heat from two people sleeping in a room with poor
airflow. We added a small fan on a schedule. Data-driven home improvement.

What I'm adding in January: a whole-home power monitor. The Emporia Vue was just
announced but isn't shipping until next year; in the meantime I'm looking at a
current transformer clamp setup on the main panel with an ESP32 reading it. Energy
monitoring was on the list for most of this year and I keep deferring it. The KiCad
board for it is in progress.
