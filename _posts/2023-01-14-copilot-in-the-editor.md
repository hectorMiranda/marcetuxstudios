---
layout: post
title: "GitHub Copilot in the editor for a month"
date: 2023-01-14
author: marcetux
tags: [tooling, copilot, rust, productivity]
---
I turned on GitHub Copilot in VS Code around the same time ChatGPT launched and the
two are different experiences worth distinguishing. ChatGPT is a conversation;
Copilot is a ghost typing next to you, anticipating what comes after the cursor. One
you consult, the other just shows up.

For boilerplate-heavy work — serialization structs, repetitive match arms, test
fixtures that follow a pattern — Copilot is genuinely fast. I'll type the struct name
and the comment describing what it is, and the first field suggestion is usually right.
Accept, tab through, done in seconds. For Rust specifically it also handles
lifetimes and trait implementations with more accuracy than I expected, probably
because Rust code tends to be explicit about types and Copilot feeds on explicit signals.

Where it falls flat is anything with real domain knowledge: Casper's deploy structure,
our internal RPC types, the behavior expected by the contract runtime. It suggests
plausible-looking code that uses the wrong field names or calls APIs in the wrong order,
and the compiler catches most of it, but the review step is non-negotiable. The rule
I've landed on: Copilot drafts, I read and own. Never accept a suggestion I can't
explain. That's not a limitation — that's just what using a tool correctly looks like.
