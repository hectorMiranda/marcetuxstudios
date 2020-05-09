---
layout: post
title: "Deno 1.0 and what it says about what Node got wrong"
date: 2020-05-08
author: marcetux
tags: [javascript, deno, node, runtime]
---
Deno 1.0 shipped this week and Ryan Dahl's ten-regrets talk from JSConf EU 2018
finally has an answer to "so what would you do differently?" The answer is Deno:
TypeScript first-class, no `node_modules`, a permission model that requires you to
grant network or filesystem access explicitly, and browser-compatible URLs for module
imports instead of a registry. It's a considered re-evaluation, not a rewrite for
its own sake.

The permission model is the thing that sticks with me from a security standpoint.
`node script.js` runs with whatever access the process has. `deno run script.ts`
refuses to make network calls unless you pass `--allow-net`. A supply-chain attack
via a malicious package can exfiltrate secrets in Node because the package runs with
your credentials; in Deno it can't make outbound calls without a flag you had to
consciously set. That's a meaningful shift in the threat model.

Whether Deno displaces Node is the wrong question for now — npm's gravity is
enormous and most of the JavaScript world's investment is there. The right question
is whether it makes specific use cases cleaner: quick scripts, secure sandboxed
tasks, TypeScript tooling where the setup overhead of a Node/ts-node project
exceeds the script's value. For those, yes. For the production API server backed by
Express and ten years of npm packages, not yet.
