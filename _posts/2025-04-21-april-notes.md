---
layout: post
title: "April notes"
date: 2025-04-21
author: marcetux
tags: [meta, retrospective, studio]
---
The CO2 sensor boards are in and reporting. The studio is properly monitored now — CO2,
temperature, humidity, power draw from the UPS, and the Grafana dashboards for all of
it running on the Pi cluster in the rack. It went from a construction project to a
functioning workspace faster than I expected, and I'm actually working better in it than
I expected, too. The bench is where the thinking happens and now the bench is good.

On the consulting side: RAG quality audits have become a repeatable engagement type.
Every team that's shipped a RAG feature has retrieval problems they haven't looked at
closely. The pattern is so consistent that I've started the engagement with a recall-at-
k measurement before any conversation about the generation side. MCP adoption is moving
fast and the ecosystem quality variance I flagged is real — more on that once I have
concrete examples I can share.

Structured output from the model APIs has quietly made a whole class of defensive code
unnecessary. Grateful to whoever decided to ship constrained decoding — it's the kind
of boring improvement that makes the work more reliable without any drama.
