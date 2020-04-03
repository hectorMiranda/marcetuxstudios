---
layout: post
title: "Splunk saved searches as on-call infrastructure"
date: 2020-04-02
author: marcetux
tags: [observability, splunk, oncall, devops]
---
Three weeks into fully remote on-call, the thing that's kept me sane is the set of
Splunk saved searches I built over the past year without fully appreciating what they
were doing. An error spike at midnight used to mean SSH and grep; now it means opening
a dashboard that's already running the searches I'd have typed anyway, scoped to the
right time window, filtered to the right service.

The searches that matter are not clever. They're the four or five queries you run
every single time something is wrong: error rate per service per five-minute window,
slow queries above a threshold, auth failures over baseline, the specific log pattern
that means the payment queue is backing up. The value isn't the query language — it's
having them named, saved, and reachable in one click at 2 AM when you're not fully
awake.

The discipline I'm pushing on the team: when you triage an incident and run a Splunk
query to diagnose it, that query goes into the saved-searches library before you close
the incident. The next time that failure mode appears, the investigation is already
done. Runbooks are the "what to do"; saved searches are the "what to look at first."
Together they're most of what on-call actually requires.
