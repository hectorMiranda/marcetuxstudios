---
layout: post
title: "October notes"
date: 2013-10-31
author: marcetux
tags: [meta, retrospective]
---
Q4 predictably compressed the personal project time. The Arduino/Pi ambient sensor
project is wired up but the software side is still the logger script from the first
bench build — I haven't gotten around to the comparison analysis. The hardware is done;
the data is sitting there; the time to look at it hasn't appeared yet.

Work-side: the API versioning strategy is now policy, not just my practice — the team
agreed on URL versioning with namespace separation after a half-hour discussion that
was shorter than I expected. Entity Framework no-tracking is now a lint comment in the
code review checklist. The Bitcoin traffic patterns from inside the CDN are the clearest
illustration I've seen of "cache-friendly design pays off under load" in a real,
observable dataset.

November: the holiday traffic season starts and CDN edge load goes up. I want to look
at what happens to the WebSocket connections during a load spike — specifically whether
the SignalR clients reconnect gracefully when a web server restarts under the rolling
deploy we now run during high-traffic periods. Also: I've been reading about Redis
Lua scripting for atomic multi-step operations and want to test whether it applies to
the session-token renewal flow.
