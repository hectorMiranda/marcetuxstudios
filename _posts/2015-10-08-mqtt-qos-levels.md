---
layout: post
title: "MQTT QoS levels at home and why they matter"
date: 2015-10-08
author: marcetux
tags: [mqtt, esp8266, electronics, home-automation]
---
The home sensor network has been running for nine months and the one issue I've noticed
is occasional missed readings from the bathroom temperature node during broker restarts.
The root cause is QoS level 0 — the default, fire-and-forget delivery — which makes no
delivery guarantee. The message is sent; whether it arrives is the network's problem.

MQTT defines three QoS levels. **QoS 0** is fire-and-forget. **QoS 1** guarantees at-
least-once delivery: the receiver acknowledges, and the sender retains and retransmits
until acknowledged. This means duplicates are possible but loss is not. **QoS 2** adds a
four-part handshake to guarantee exactly-once delivery, at the cost of two more round
trips per message.

For sensor readings, QoS 1 is right: I can tolerate a duplicate temperature reading but
not a gap in the history. The change on the ESP8266 nodes is a single parameter in
`mqtt.publish(topic, payload, retained, 1)` — the last argument is the QoS level. On
the Mosquitto broker, no configuration change is required; the broker honors the QoS
requested by the publisher. The InfluxDB subscriber's upsert-on-timestamp logic handles
duplicates gracefully — same timestamp, same value, the second write overwrites the
first without creating a duplicate row. The combination — QoS 1 for guaranteed delivery,
idempotent writes for duplicate safety — is the pattern for any reliable message pipeline.
