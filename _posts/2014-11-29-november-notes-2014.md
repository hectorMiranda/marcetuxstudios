---
layout: post
title: "November notes 2014"
date: 2014-11-29
author: marcetux
tags: [meta, retrospective]
---
November had two tech announcements that will matter for a while. .NET open source
under MIT changes what the Microsoft ecosystem becomes, and I'm watching with the
same interest someone has when they leave a city and it starts gentrifying after. AWS
Lambda I'm watching more immediately — the event-driven serverless model fits at least
three things in the Spark architecture better than the current SQS worker approach.

The home sensor API cleanup was satisfying in the way that refactors from "working" to
"correct" usually are. The bug the tests caught would have silently mislabeled sensor
readings. It would have eventually corrupted a week of data before I noticed, and I
probably wouldn't have traced it back to the validation logic. Tests find bugs, that's
not a controversial statement — but it's still satisfying every time it happens.

December: I want to run a load test against the Spark API endpoints I own, because I
don't have a good intuition for where the capacity ceiling is. Six months of production
data should give me a baseline query plan to compare against. And the home dashboard
is getting a proper frontend — gnuplot cron graphs are not a dashboard, they're a
placeholder.
