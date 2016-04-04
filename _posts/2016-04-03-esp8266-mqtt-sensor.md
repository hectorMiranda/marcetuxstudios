---
layout: post
title: "Kitchen sensor sending temperature to MQTT"
date: 2016-04-03
author: marcetux
tags: [esp8266, mqtt, electronics, home, hardware]
---
The kitchen sensor board I've been talking about since January is actually sending data
now. An ESP8266 on the breakout I built, a DHT22 for temperature and humidity, a small
LiPo cell and TP4056 charger for battery, and a few lines of Arduino-framework code to
wake up every five minutes, read the sensor, publish to Mosquitto on the Pi 3, and go
back to sleep. The whole thing fits in a 3D-printed case I borrowed a design for and
barely modified.

The MQTT side is satisfying: the topic is `home/kitchen/temperature` and `home/kitchen/
humidity`, the payload is plain JSON, and anything that subscribes to those topics gets
the data without knowing anything about the sensor. The Pi 3 broker is also
logging to InfluxDB via a bridge script; Grafana reads the InfluxDB and the dashboard
finally shows something other than test data. I can look at the last 30 days of
kitchen temperature and there is no practical reason for this and I don't care.

Deep sleep is what makes battery life reasonable. The ESP8266 in modem-sleep mode burns
roughly 70 mA; in deep sleep it's closer to 10 µA. Five-minute intervals with a fast
wake-read-publish-sleep cycle means the radio is on for maybe three seconds out of 300.
The LiPo lasts weeks between charges. Publish what you need, sleep the rest of the time.
