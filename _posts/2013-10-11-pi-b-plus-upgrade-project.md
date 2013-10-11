---
layout: post
title: "The Raspberry Pi Model B+ and a reason to upgrade"
date: 2013-10-11
author: marcetux
tags: [raspberry-pi, electronics, hardware, linux]
---
The original Model B running the bench logger data collector has been rock-solid for
months, which is exactly the reason I want to leave it alone. But a second Pi arrived
this weekend for a different project — a small home automation sensor hub — and building
the new thing on a clean device with fresh assumptions is a better idea than growing
the logger Pi's single script into a general-purpose server.

The setup is the same: Raspbian, the Python serial reader for the UART, the cron jobs
for rotation. The difference is I wrote an Ansible playbook this time, based on the
Vagrant provisioning work from May. `ansible-playbook -i inventory pi-setup.yml` from
my laptop, target the Pi's IP, and the playbook installs the packages, copies the
collector script, and registers the cron jobs. The Pi is in a known, reproducible state
from the first boot.

The automation sensor hub is a separate project: an Arduino wired to a DHT22
temperature/humidity sensor, reporting over UART to the second Pi, which will log to
the same CSV-plus-daily-rotation scheme. The interesting part will be comparing the
bench readings (voltage and temperature during electronics work) against the ambient
readings (temperature and humidity in the room) to see whether the bench heater is
doing anything detectable. Two sensors, two Pis, one question.
