---
layout: post
title: "Angular 9 and Ivy, what it means in practice"
date: 2019-10-04
author: marcetux
tags: [angular, frontend, typescript, performance, tooling]
---
Angular 9 is in release candidate and Ivy — the new compilation and rendering engine — ships as the default. We've been watching Ivy since the opt-in appeared in Angular 8, running it in dev builds to find any breaking changes before they landed on us. The RC is stable enough that I've been building the banking portal's dev branch against it for two weeks.

The concrete improvements that matter to us: smaller bundle sizes through better tree-shaking. Ivy compiles each component independently — the old View Engine compiled a module at a time, which made it harder to eliminate unused code. With Ivy, components are proper units of compilation, and the linker can drop anything not reachable from what's actually rendered. The banking portal's lazy-loaded payment module shrank by about 15% without any code changes. That's meaningful on a corporate laptop over a VPN.

The breaking changes in Ivy are in the edges: some advanced use of Angular internals and dynamic component creation had to be updated. We had one use of `ViewContainerRef` that relied on an implementation detail of View Engine that Ivy changed. Finding it early in the dev build meant fixing it in a branch PR rather than in an incident. The discipline of running pre-release on a dev branch and merging it regularly is the same as tracking a dependency's prerelease — you find the breaks in cheap environments, not in staging the week before a release.
