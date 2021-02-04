---
layout: post
title: "Vite and a build tool that gets out of the way"
date: 2021-02-03
author: marcetux
tags: [frontend, vite, tooling, javascript]
---
A weekend project outgrew the single-file prototype stage and needed a real build.
I reached for webpack, muscle-memory, and then stopped and tried Vite instead
because I was tired of configuring webpack and I'd seen enough positive noise to
warrant a few hours. Four hours later I had the same output with no configuration
file and a dev server that reloads before I've switched focus to the browser.

The trick Vite exploits is native ES modules in the browser. In dev mode it doesn't
bundle at all — it serves each module file individually and lets the browser's module
system do the graph resolution. That means the dev server startup is nearly instant
and a change to one file reloads only that file's module, not a rebuilt bundle. For
a small-to-medium project the difference between "I edited and saved" and "I can
test it" collapses to the time it takes to switch windows.

The production build uses Rollup, which is a real bundler, so you don't sacrifice
the output quality for the dev-time convenience. I've been watching tools try to
solve this trade-off for years — fast dev builds that produce good production
bundles — and Vite is the first one that doesn't feel like you're paying a hidden
fee somewhere. Still keeping webpack for the bank's Angular monorepo because
changing build tooling mid-product is a different conversation, but for net-new
side projects Vite is the obvious choice.
