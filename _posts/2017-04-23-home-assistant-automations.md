---
layout: post
title: "Home Assistant automations that actually do something useful"
date: 2017-04-23
author: marcetux
tags: [homeassistant, iot, raspberrypi, home]
---
Two months in, Home Assistant's sensor graph is satisfying but the automations are where
it earns its keep. The first real automation I wrote that wasn't a demo: if the office
temperature sensor goes above 26°C between 9am and 6pm on a weekday, turn on the
portable AC unit via a smart plug. Simple, useful, runs without me thinking about it.

The YAML syntax for automations is readable once you get the trigger/condition/action
model in your head. **Trigger** is what fires it: a sensor crossing a threshold, a time
pattern, a state change. **Condition** is the guard: only run if it's a weekday, only
if someone is home. **Action** is what happens: call a service, turn on an entity, send
a notification. The separation is cleaner than I expected — I can swap a trigger without
touching the action logic.

The harder automation was presence-based. I want the house to enter "away" mode when
everyone leaves, without GPS tracking of family members through their phones. The answer
is the router's DHCP table: Home Assistant has a device-tracking integration that
watches for MAC addresses on the network. Phone absent from the network for fifteen
minutes means the person is likely gone. It's approximate and it works well enough. The
edge case is someone sitting in the car in the driveway — but if the AC runs for fifteen
extra minutes, that's an acceptable false positive.
