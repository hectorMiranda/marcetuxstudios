---
layout: post
title: "Remote incident response and the async gap"
date: 2020-04-20
author: marcetux
tags: [oncall, process, remote, devops]
---
A month of fully remote on-call has clarified the gap that the office concealed.
An office incident has an improvised war room — people converge, state gets shared
by talking out loud, decision-making is visible. A remote incident has a Teams
channel with ten people typing simultaneously, no shared mental model of who's doing
what, and a lot of "checking now" messages that create the illusion of progress.

The fix we've landed on is explicit incident commander rotation. One person owns
coordination: they declare the incident, assign the investigation threads, write the
status update every fifteen minutes, and hold the decision on remediation. They're
not necessarily the best engineer in the room for the technical problem — they're
the one making sure the technical work doesn't become five people independently
diagnosing the same thing. Everyone else closes the channel except for updates and
asks.

The behavior change was harder than the process change. Engineers want to investigate,
not coordinate, and the instinct when something is on fire is to jump in rather than
wait for an assignment. The discipline of "stop, read the current status update,
then do the one assigned thing" is what makes a remote incident manageable. It's
the same discipline that makes asynchronous work in general manageable: shared
written state replaces the ambient awareness of a shared physical space.
