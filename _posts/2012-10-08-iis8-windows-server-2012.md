---
layout: post
title: "Notes from moving to IIS 8"
date: 2012-10-08
author: marcetux
tags: [iis, windows-server, dotnet, ops]
---

We're standing up Windows Server 2012 boxes and the IIS 8 changes are more than a
version bump. Two features are already earning their keep for a .NET shop running
always-on portals.

**Application Initialization** ("warmup") is the one I'm happiest about. The classic
sin of IIS is the first request after an app-pool recycle paying the JIT-and-load
tax — one unlucky user waits ten seconds. IIS 8 can pre-load the app and even hit a
warmup URL so it's hot *before* real traffic arrives. Combined with a sane
recycling schedule, the cold-start cliff mostly disappears.

**Centralized SSL certificate store** is the other: certs in one share instead of
hand-installed per machine, which matters the moment you have more than one web
server behind a load balancer and are tired of certificate drift.

The CPU throttling per app-pool is nice too — noisy-neighbor protection when you
co-host. None of this is glamorous. All of it is the difference between a portal
that's reliably up and one that's up "usually."
