---
layout: post
title: "Migrating to Webpack 2 from the Angular-CLI defaults"
date: 2017-01-18
author: marcetux
tags: [webpack, angular, tooling, frontend]
---
The internal seller dashboard started with Angular CLI's default setup, which worked
until it didn't — specifically until we needed to handle multiple entry points for
different seller roles and a lazy-loaded admin section. The CLI's eject command spits
out the raw Webpack config, which is the thing you actually need to understand before
you can change it.

Webpack 2 is a real improvement from the 1.x config I've worked with before. Tree
shaking is native now — it actually reads ES2015 module imports statically and removes
exports nothing references, which dropped the vendor bundle noticeably. The router-level
lazy loading Angular sets up composes with Webpack's code splitting naturally: the
router chunk gets loaded on demand, not on startup. Configuring it took an afternoon and
cut the initial bundle by about a third.

The gotcha was the loader configuration syntax change. Webpack 2 uses `use: ['loader']`
where 1.x used `loaders: 'loader!loader'`. The old string syntax silently does nothing
in some edge cases, which produced the most mystifying missing-asset errors before I
read the migration guide. The guide is good; read it before copying Stack Overflow
answers that are subtly 1.x. "Works in dev" and "works in prod" being different is
Webpack telling you your config isn't done.
