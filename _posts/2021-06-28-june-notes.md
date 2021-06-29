---
layout: post
title: "June notes"
date: 2021-06-28
author: marcetux
tags: [meta, retrospective]
---
June felt like a month where the interesting things happened at the edges of the
day job rather than in it. Copilot preview was the most surprising — I expected
to be more skeptical than I am. It's a real productivity tool for the mechanical
parts of the work, and I've watched it write a complete test class from a comment
in under a second. Whether it's a net positive depends on whether you're careful
enough to catch what it gets subtly wrong, which is a skill question as much as a
tool question.

The service-mesh evaluation closed with a "not now" decision that I think was
correct and that I'll need to revisit in eighteen months when the team is bigger.
The caching rationalization was less glamorous but probably more impactful — four
incoherent cache layers is not better than two coherent ones. The DFM lesson from
the PCB fab was one I've learned before and clearly needed to learn again.

Rustlings finished in the background during lunches and weekends. The language is
clicking in a way it didn't the previous two times I tried. The ownership model
stopped feeling like a restriction and started feeling like a design language — a
way of expressing what the code is allowed to do with data. I don't have an excuse
to use it at work yet, but I'm paying closer attention to the embedded Rust
community than I was last month. Something is pulling there.
