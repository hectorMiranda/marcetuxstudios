---
layout: post
title: "A first look at Vue.js from an Angular developer"
date: 2016-02-15
author: marcetux
tags: [vue, javascript, frontend, spa]
---
I've been watching Vue.js from a distance for about a year — Evan You's "progressive
framework" pitch was interesting but Angular was doing the job, so there was no reason
to switch. A side project gave me an excuse to actually use it rather than just read
about it, and after a weekend I understand why it's gathering momentum.

The entry point is gentler than Angular's. You include a script tag, add `v-bind` and
`v-on` to existing HTML, and something works. You don't need a build step, a CLI, a
module system, or a mental model of zones and change detection. The reactivity is
automatic — mutate a property on the data object, the DOM updates. Angular's two-way
binding works similarly but behind more abstraction. Vue's model is closer to the
surface and easier to reason about.

What I'm not sold on yet: the ecosystem is thin compared to Angular's, the community
tooling is immature, and a `.vue` single-file component requires a build step once you
want real structure. For a side project it's a pleasure; for a production app at work
I'd need more confidence in the stability story. But it solves the "I just need a
component that works" problem faster than any framework I've tried. If the ecosystem
matures through this year, the "growing threat to Angular" narrative will earn itself.
