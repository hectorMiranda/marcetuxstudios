---
layout: post
title: "GitHub Copilot preview: a week of notes"
date: 2021-06-07
author: marcetux
tags: [tooling, github, productivity, developer-experience]
---
Got access to the GitHub Copilot technical preview this week and I've been running
it in VS Code on the side projects. First impressions are genuinely mixed, which
surprised me — I went in expecting either "interesting toy" or "overhyped." It's
something more interesting than either: a tool that's useful in a specific slice of
the work and actively in the way for another slice.

The slice where it lands well is boilerplate at the boundary — test scaffolding,
interface implementations, repetitive DTO mappings, the kind of code where the
structure is obvious and the interesting decision was made three files ago. I wrote
a comment describing a method, hit enter, and the suggestion was 90% right. I
accepted it and moved on. That's a real speed-up for code that I'd have written
identically but with more keystrokes.

Where it's less helpful is the code that requires understanding the invariants of
the specific system: why a particular field is nullable, why a cache TTL is exactly
that value, why we call service A before service B. Copilot suggests code that
looks plausible and compiles, and catching the subtle semantic error in a plausible-
looking suggestion takes more attention than just writing the code. It's a tool, and
like most tools, it's most valuable to people who already know the job — because
they can catch what it gets wrong. I'll keep running it and see if the trust builds.
