---
layout: post
title: ".NET 6 ships and the Minimal APIs verdict"
date: 2021-11-02
author: marcetux
tags: [dotnet, aspnet, csharp, architecture]
---
.NET 6 went GA on November 8th and the internal service I'd been running on RC
went to production the same day. No drama — the RC had been stable enough that
GA felt like a version number bump with extra ceremony. The LTS designation matters
for the bank: three years of support means this is the platform we commit to, not
an experiment. That's a different conversation from the early preview work.

Minimal APIs are now production-stable and I want to nail down what they're good
for after six months with them. For a microservice with a well-defined API surface —
ten to fifteen endpoints, owned entirely by one team, consumed by services you also
control — they're the right choice. Startup time is faster, the code surface area
is smaller, and new developers land on the routing logic without navigating a
controller hierarchy. The discipline of keeping endpoint registrations organized
falls on the team rather than on the framework, which is a trade I'm comfortable
making for teams that have the discipline.

The one addition in .NET 6 I didn't expect to value as much as I do: `DateOnly` and
`TimeOnly`. The bank's codebase has at least eight places that stored a date as a
`DateTime` with an arbitrary time component set to midnight UTC, and the bugs from
mishandling that time component in timezone edge cases are well-documented in old
incident reports. `DateOnly` is a first-class type that contains exactly what it
says. It seems minor until you've debugged a date-comparison bug at 2 a.m.
