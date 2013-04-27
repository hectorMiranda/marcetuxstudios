---
layout: post
title: "April notes"
date: 2013-04-27
author: marcetux
tags: [meta, retrospective]
---
April was a connecting-things month. OAuth2 is in and working — the flow that looked
like diagrams became a few well-understood controller actions. Angular UI-Router handles
the nested dashboard state in a way the built-in router never could. Grunt now owns the
full front-end build and MSBuild calls it, so CI can't ship a broken asset set anymore.

On the smaller-but-meaningful side: ETags took a round-trip down from a full response
to a 304, DI explained as a principle before a framework, and asset fingerprinting
makes cache-forever URLs safe to give. These are the kind of posts I write when
something I'd been doing by rote finally *makes sense* — the understanding is the
thing, not the tool.

The v1.1 PCB layout is done; sending to fab in the next day or two. The test points
and the proper UART header should make v1.1 a board I can actually use at the bench
instead of a board I'm always probing awkwardly. May: the reporting surface ships, I
want to revisit the SignalR backplane question as we consider a second web server, and
the Pi keeps collecting logger data.
