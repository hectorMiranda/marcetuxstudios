---
layout: post
title: "ChatGPT two weeks in: what it is and is not good for"
date: 2022-12-06
author: marcetux
tags: [ai, chatgpt, productivity, tools]
---
Two weeks of daily use. I owe the considered take I promised in the November 30th post.

The thing ChatGPT is genuinely good at, in my use: explaining concepts I half-understand
at exactly the level I ask for, drafting first passes at documentation I'd otherwise
stare at a blank page for, and working through error messages with me by talking through
the possibilities methodically. I've also used it to get un-stuck on Rust lifetime
problems by describing the scenario — it doesn't always get the answer right but it
usually names the right concept to look up, which shaves forty minutes off the "I don't
even know what to search for" problem.

Where it breaks down: anything that requires current information (training cutoff), any
code it hasn't seen patterns of (it invents plausible-looking wrong APIs), and any
domain where wrong sounds as confident as right. The Casper contract API is obscure
enough that it will hallucinate entry point names and type signatures with full
confidence. The rule I've landed on: treat it like a knowledgeable colleague who
occasionally makes things up and always sounds sure. Verify anything that matters.

The engineering productivity question is real and I don't have a clean answer. Some
days it saves me an hour; some days I spend twenty minutes verifying a wrong answer I
should have looked up myself in ten. The calibration is the work — learning when to
reach for it and when to go straight to source. Same as every other tool.
