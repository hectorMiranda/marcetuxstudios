---
layout: post
title: "WASM components and the module system we actually needed"
date: 2024-06-08
author: marcetux
tags: [webassembly, wasm, architecture, systems, rust]
---
I've been watching the WASM component model spec mature and I want to articulate why
it's more interesting than "compile your code to run in the browser." The component
model is a cross-language module system with a defined interface description language
(WIT), bidirectional linking, and a capability-based security model. That's a
different proposition from the original WASM pitch.

Concretely: a WASM component exposes interfaces defined in WIT — WebAssembly
Interface Types. A component written in Rust and a component written in Go can import
from each other if both implement or consume the same WIT interface. The type system
is at the component boundary, not inside any one language. The security model is
capability-based — a component only has access to capabilities explicitly granted to
it, not to arbitrary OS APIs. This is safer than a native shared library and more
portable than any current package ecosystem.

Where I see this mattering practically in the next few years: plugin systems and
extension points in platform products. If you're building an internal platform and
you want to let teams extend it safely, WASM components give you isolation without
a subprocess boundary. The team writes their extension in whatever language they
know; it links at the WIT interface; it can't access what it isn't given. That's an
attractive alternative to "submit a PR to our Go monorepo and we'll merge it when
we get to it." The tooling isn't there yet, but the spec is converging.
