---
layout: post
title: "InfluxDB for sensor history on the Pi"
date: 2015-03-02
author: marcetux
tags: [influxdb, raspberrypi, mqtt, home-automation, timeseries]
---
Mosquitto retains the last value per topic but forgets history on restart, which meant
the living-room temperature chart I wanted showed exactly one dot. InfluxDB is the
fix — a time-series database that runs comfortably on the Pi 2 and understands that
sensor readings are a stream of timestamped measurements, not rows in a table.

The integration is a small Python subscriber that listens on `home/#`, parses the topic
into a measurement name and tags, and writes to InfluxDB over its HTTP API. The InfluxDB
line protocol is pleasantly minimal: `measurement,tag=value field=value timestamp`.
One call per message, no schema to define ahead of time — InfluxDB infers types from
the first write. After 48 hours of collection I had enough data to draw a real chart.

Querying is SQL-like: `SELECT mean("value") FROM "temp" WHERE "room"='living' AND
time > now() - 24h GROUP BY time(15m)`. The `GROUP BY time(15m)` bucketing is where
time-series databases earn their keep — aggregating across time buckets is something
general-purpose databases can do but don't do with this syntax, and "the last day of
temperature at 15-minute resolution" executes immediately. Grafana was the obvious
dashboard layer once I had data in InfluxDB; it speaks the query language natively and
the chart was five minutes of configuration. The shelf-Pi now shows me what I wanted.
