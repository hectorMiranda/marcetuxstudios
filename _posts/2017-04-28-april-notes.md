---
layout: post
title: "April notes"
date: 2017-04-28
author: marcetux
tags: [meta, retrospective]
---
April was dense on the infrastructure side. The Lambda webhook receivers are in
production and the ops team has stopped asking me why a seller's sync is stuck, because
the health dashboard tells them before they think to ask. Those two outcomes together
feel like a month's work that paid off immediately, which doesn't always happen.

The Webpack 3 scope hoisting result was a pleasant surprise — not something I was
expecting to spend time on, but a 10% bundle reduction for a single config line is the
kind of ROI I don't second-guess. The PostgreSQL JSONB move is the more significant
architectural decision and I think it will age well, but I want to see six months of
production traffic on it before I write it up as a recommendation.

Home side: the temperature automation has been running for two weeks and the office is
measurably more comfortable. Small wins. The Pi cluster is getting some workload now
— I'm using it to run the Home Assistant and Mosquitto, which moved off the standalone
Pi onto the cluster. The standalone Pi can now be a dedicated sensor hub for a project
I have in mind for May.
