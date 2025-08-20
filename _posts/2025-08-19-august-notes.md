---
layout: post
title: "August notes"
date: 2025-08-19
author: marcetux
tags: [meta, retrospective, studio]
---
The spectrum monitor is working end to end: antenna, RF front-end, SDR, feature
extraction, classifier, and a Grafana dashboard that shows signal activity over time.
Three years of intermittent hardware work, resolved in a summer. The data is interesting
— there's a persistent narrowband carrier in the 433MHz band that I haven't identified,
probably a neighbor's weather station or gate opener, but the classifier is logging it
consistently.

The agent orchestration and Terraform posts came from real client work, which is the
best source of material. The orchestration pattern is something I've built variations
of three times now and finally have the stable version of. The Terraform advice is what
I wish someone had told me the first time I was responsible for an IaC migration at a
small team.

The UPS battery situation: replacement ordered. Monitoring the battery health through
Home Assistant means I had the degradation curve in Grafana before I ever opened the
UPS panel to look at the LED status. Instrumentation pays off in the most mundane ways.
