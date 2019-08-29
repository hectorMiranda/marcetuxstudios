---
layout: post
title: "August notes"
date: 2019-08-28
author: marcetux
tags: [meta, retrospective]
---
August has been about solidifying things that were still rough around the edges: Worker Service model is almost production-ready waiting on the 3.0 GA, the Splunk dashboards are now something operations actually opens without being asked, and the Docker hygiene posts summarize what I've been quietly fixing across the team's repositories over the past two months. The security team relationship is probably the thing I'm most satisfied with — it took six months to build enough trust that the reviews are collaborative rather than adversarial.

Home side: the energy monitor is producing data I'm actually acting on. July's HVAC numbers prompted me to audit the apartment for things I could switch off during peak hours. The Pi 4 is running the full monitoring stack without complaint, which was the main unknown. I ordered a second Pi 4 for the home lab because at $55 for the 4 GB model, having a spare that could take over critical services is worth it.

September is going to be dense. .NET Core 3.0 should go GA — I'll be migrating the reconciliation Worker the week it ships. Blazor Server is also supposed to land in 3.0, and I've been watching it skeptically since the experimental drop. The idea of running C# on the server and having the UI respond to browser events over a SignalR connection is either elegant or a distributed state machine I don't want to debug. Reading the docs this weekend.
