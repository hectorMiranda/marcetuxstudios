---
layout: post
title: "The golden path is not optional for teams past a dozen engineers"
date: 2025-06-09
author: marcetux
tags: [platform, architecture, devops, consulting, process]
---
I've done platform consulting for small teams where the recommendation was "one person
maintains a good template and documents the decisions." That advice has a ceiling. Around
fifteen to twenty engineers, the informal "we all know how we deploy things" breaks down
because people hired in the last six months don't know, and the senior engineers who do
know are spending meaningful time answering deployment questions instead of building
features.

The golden path is the formalization of what you've been doing informally: a curated,
opinionated way to create a new service, deploy it, observe it, and on-call it that just
works if you follow it. The key word is opinionated. A platform that offers twelve
deployment patterns for twelve different preferences is not a platform; it's an API that
still requires the user to make hard choices. The value of the golden path is that the
hard choices were made once, by people with the full context, and new team members can
skip them.

The cost of building a golden path is upfront investment that feels expensive before it
pays off. The cost of not building one is distributed and hidden — it shows up as slow
onboarding, inconsistent incident response, and senior engineers fielding "how do I do X"
questions they've answered forty times. Hidden costs are real costs. The golden path pays
for itself in the first round of new hires.
