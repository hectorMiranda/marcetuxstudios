---
layout: post
title: "Migrating console workers to hosted services, lessons from the field"
date: 2019-09-13
author: marcetux
tags: [dotnet, workers, migration, aspnetcore, devops]
---
Three services migrated to the Worker Service pattern this month, each with its own character and its own migration surprise. The reconciliation service I mentioned last week was the cleanest. The account aggregation service had a subtler problem: it used a static class for its cancellation token because the original console app didn't have a proper lifetime concept, and the Worker model's `stoppingToken` exposed that design assumption immediately.

The fix wasn't complicated — inject a class rather than reach for a static — but it surfaced a pattern I've seen repeatedly: console apps written without DI have state stored in places that don't compose with hosted services. The migration exposed the technical debt; it didn't create it. That's actually one of the useful things about migrating: the friction points are where the old code was doing something that only worked because it owned the whole process. A hosted service model expects you to be one tenant among several in the host, and the code has to earn that.

The operational benefit is now visible: all three services have health checks registered with the host, emit structured logs via the same Serilog configuration as the API services, and respond to `SIGTERM` gracefully. Previously each had its own shutdown logic written slightly differently, which made the on-call runbook longer than necessary. Now the paragraph about worker shutdown is the same for all of them. Boring uniformity is the goal.
