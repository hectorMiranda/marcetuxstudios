---
layout: post
title: "SASS mixins and building responsive components without repeating media queries"
date: 2013-06-17
author: marcetux
tags: [css, sass, responsive, frontend, tooling]
---
The dashboard has grown to a point where the same breakpoint values appear in a dozen
places. `@media (min-width: 768px)` is sprinkled through five different component files,
each with its own numerical literal, which means "change the tablet breakpoint" is a
grep-and-replace exercise prone to missing one. SASS mixins made the problem
mechanical.

The pattern I settled on is a set of named breakpoint mixins: `@mixin respond-to($bp)`
that accepts a name like `tablet` or `desktop` and emits the right media query from a
private map of values. Components include the mixin instead of writing the query
directly. Change the breakpoint value in one place — the map at the top of the
variables file — and the change propagates everywhere at the next compile.

The second benefit is legibility. `@include respond-to(tablet)` reads as intent;
`@media (min-width: 768px)` reads as an implementation detail. The intent survives
longer than the pixel value — if the definition of "tablet" shifts next year, the
mixin usage doesn't have to. I've been treating CSS as the dumb output of a dumb
pipeline for too long; SASS lets me apply the same DRY habits to styles that I apply
to the C# code a floor above them in the stack.
