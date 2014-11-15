---
layout: post
title: "Cleaning up the home sensor API"
date: 2014-11-15
author: marcetux
tags: [ruby, sinatra, iot, raspberrypi, hardware, maker]
---
The Sinatra app on the Raspberry Pi that collects sensor readings has been running for
two months and the code shows it — it was written in an evening and it reads that way.
This weekend I cleaned it up, partly because the messiness was bothering me and partly
because I want to add new sensors and the current structure would make that painful.

The changes were structural rather than algorithmic. Routes that were inline lambdas
became classes that follow the same service object pattern I've been using at work.
A `Reading` class handles validation and database insertion; the route validates,
calls the service, and responds. The database access was raw SQL string building —
now it uses Sequel's dataset API to compose queries safely. A schema migration script
that was a comment in the README became an actual Sequel migration file.

The test suite is new — there was none. Rspec with Rack::Test lets me hit the Sinatra
routes in a test without starting a real server. Three tests per route: happy path,
missing params, invalid sensor ID. Forty-five minutes of writing tests found one
bug (the sensor ID validation was backwards) and one performance issue (I was querying
for the unit on every insert instead of caching it). Small codebase, short test run,
immediate payoff. The gap between "code I'd be embarrassed to show someone" and "code
I'd be comfortable reading in six months" is not as large as it sometimes feels from
the outside.
