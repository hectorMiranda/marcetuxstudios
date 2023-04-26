---
layout: post
title: "April notes"
date: 2023-04-25
author: marcetux
tags: [meta, retrospective]
---
April was heavier on Rust than on LLM experiments, which might be the job doing what
it should — pulling my attention toward the thing I'm paid to build well. The Tokio
rewrite cleaned up the client in ways I'm proud of: cleaner concurrency, no blocking
calls on the main thread, and a test harness that can inject mock futures for the
network calls. That's the kind of refactor you do before something gets complicated,
not after.

The embedding benchmarks surprised me. I assumed "good enough" was cheaper than it
turned out to be, and the 16-point gap between MiniLM and Ada on my actual queries
was a real number, not a benchmarking artifact. That changes how I think about the
"just use the small local model" shortcut. Sometimes the shortcut costs quality in
ways that compound.

The LangChain chain-vs-agent distinction sounds obvious written down but I've watched
smart engineers pick agents for problems that wanted chains and end up debugging
nondeterministic behavior that shouldn't exist. The framing I landed on — can I draw the
flowchart before I write the code? — is the kind of thing that belongs in a design
meeting more than a blog post, but here we are.
