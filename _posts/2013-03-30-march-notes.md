---
layout: post
title: "March notes"
date: 2013-03-30
author: marcetux
tags: [meta, retrospective]
---
The boards arrived in March, and they're mostly working — one footprint wrong, one
board bodged, nine boards waiting for a corrected v1.1 layout. The Pi is now reading
the logger's UART output into a CSV, which is the minimum viable data-capture system.
It doesn't look like much but the bench now tells me things it couldn't before.

Work-side, the month was a systems-thinking month. Vagrant closes the "works on my
machine" gap; Redis kills sticky sessions and makes session state visible; Web API
model validation belongs at the entry point, not scattered inside controllers; the OWIN
middleware idea from February turned into a real, testable piece of code. Docker showed
up and I'm filing it under "compelling direction, not my platform yet."

The Grunt watcher changed my edit loop more than I expected — short feedback cycles
really do change what you're willing to try. SQL Server's `ROW_NUMBER` is the kind of
thing I wish I'd reached for two years ago instead of reinventing offset logic in
application code. April: ship the new reporting surface, revisit the OAuth2
integration I deferred from January, and start the v1.1 board layout.
