---
layout: post
title: "July notes"
date: 2013-07-28
author: marcetux
tags: [meta, retrospective]
---
July was a security and authentication month with a thread of tooling maintenance
running alongside. OAuth2 refresh tokens mean the portal keeps its session without
kicking users out; the OWIN auth pipeline is now properly understood rather than
cargo-culted; HMAC signing gives the server-to-server API a credible authentication
scheme. The post-Snowden audit from June is almost complete: one last API surface on
plain HTTP and it gets TLS.

On the tooling side, `grunt-newer` brought the watch loop back under two seconds even
with TypeScript in the build. PostgreSQL evenings are teaching me SQL things that SQL
Server's GUI had been hiding — the planner output is a better teacher than SSMS. The
Pi logger is now something I'd actually leave running indefinitely: daily rotating files,
boot-start cron, hourly summaries.

August is when Bootstrap 3 is supposed to drop, and I want to evaluate it for the next
internal tool. The new grid and the mobile-first approach are supposed to be substantial
enough that it's not a casual upgrade from 2.x. I also want to write up the full TLS
story for the team now that I understand it well enough to explain why "the CDN handles
HTTPS" isn't a complete security posture.
