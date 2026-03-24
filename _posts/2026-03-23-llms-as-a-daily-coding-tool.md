---
layout: post
title: "LLMs as a daily coding tool one year in"
date: 2026-03-23
author: marcetux
tags: [tooling, llm, workflow, productivity]
---
I've been using language model tools as a regular part of coding — not occasionally, but as a first-reach-for tool alongside the debugger and the search index — for a while now, and the "is this actually useful" question has long since been answered. The more interesting question at this point is about the shape of the usefulness: where it compounds and where it misleads, which took longer to understand than the initial "this is impressive" phase.

Where it compounds: the authoring cost of things I used to avoid because they were tedious. Writing out an exhaustive test matrix for a SCIM provisioning handler, generating the boilerplate for a new endpoint that follows the established pattern in the codebase, drafting an ADR for a decision that I already know the shape of and just need to make legible to future teammates. These aren't tasks where I don't know what to write; they're tasks where the knowing-to-writing ratio was too high to make starting feel worth it. That ratio has shifted.

Where it misleads: anything that requires deep knowledge of a specific system's behavior rather than general knowledge of a domain. If I ask about the general behavior of Auth0 refresh token rotation, the answers are useful. If I ask about a specific version-dependent behavior of a particular SDK in a particular configuration, the confidence level of the answer doesn't correlate with its accuracy. The correction, for me, has been treating those answers as leads to verify rather than facts to deploy. That's the same discipline I'd apply to a Stack Overflow answer from 2019. The tool is faster; the verification step is not optional.
