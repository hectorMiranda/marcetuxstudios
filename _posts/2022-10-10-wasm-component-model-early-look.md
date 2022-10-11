---
layout: post
title: "The WASM component model: an early look at what is coming"
date: 2022-10-10
author: marcetux
tags: [wasm, rust, component-model, standards]
---
The WebAssembly component model has been in development for a while in the W3C CG, and
this month I spent time with the drafts and the Bytecode Alliance's work on it. It's
not shipping in production tools yet — the toolchain support is experimental — but it
addresses something that's been nagging at me since I started WASM contract work: WASM
modules as they exist today are islands. You can call into a module, get a result back,
but composing two modules into a well-typed pipeline requires manual glue code.

The component model defines a standard interface description language — WIT, WebAssembly
Interface Types — that lets you describe the types that cross module boundaries. A
component exports typed interfaces and imports typed interfaces, and the toolchain
generates the binding glue automatically. Two components that agree on an interface
can be linked together without either knowing the other's implementation language or
internals. The analogy to COM interfaces is not accidental; this is the same idea,
done with the clarity of hindsight.

The relevance to smart contracts is direct: a contract ecosystem where contracts can
import and call each other with verifiable type signatures is a meaningfully safer
system than the current "serialize arguments to bytes and hope the other side reads them
right" model. The EVM's ABI encoding is the equivalent of what we have now; the
component model is what we should eventually have. I'm watching this one closely.
