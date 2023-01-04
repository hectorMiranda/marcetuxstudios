---
layout: post
title: "ChatGPT, a month later"
date: 2023-01-03
author: marcetux
tags: [ai, llm, tooling, reflection]
---
ChatGPT dropped November 30 and I haven't been able to stop thinking about it since.
Not the "it writes poetry" part — that's fine, whatever — but the moment I pasted a
gnarly Rust lifetime error into the chat box and got back an explanation that was
actually correct. That was the moment the thing stopped feeling like a toy.

A month in, here's what I actually use it for: talking through design decisions when
I'm the only engineer in the room (which happens a lot in blockchain consulting), and
getting unstuck on syntax in languages where I haven't built the muscle memory yet.
The Rust borrow checker is famously unforgiving and sometimes you just need a patient
interlocutor who will explain what "does not live long enough" means in this specific
case. ChatGPT does that reasonably well. It also hallucinates library APIs, confidently,
which has burned me twice. The workflow now: trust the explanation, verify the code.

What I don't think this is, yet, is a replacement for understanding the domain. The
answers are calibrated to "sounds right" rather than "is right," and you need enough
background to catch the plausible wrong ones. But as a tool for moving faster in
territory I know well, it's already earning its keep. January resolution: stay
skeptical but keep using it.
