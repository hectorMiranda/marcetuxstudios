---
layout: post
title: "Webpack 2 beta and tree shaking that actually works"
date: 2016-08-11
author: marcetux
tags: [webpack, javascript, frontend, tooling, bundling]
---
Webpack 2 has been in beta since April and the tree-shaking story is what I wanted to
evaluate. Tree shaking — the ability to eliminate dead code at bundle time by analyzing
static ES module imports — has been the promise for a couple of years, but previous
webpack versions didn't support ES modules natively and the shake had to go through an
intermediate tool. Webpack 2 handles ES modules directly and the shake works on the
bundles I tested.

The mechanism: ES modules use static `import` and `export` syntax that can be analyzed
without running the code. Webpack 2 tracks which exports are actually imported and marks
the rest as unused. UglifyJS then removes the unused code during minification. A
library that exports 40 utilities but you use 3 of them ships those 3 utilities and
their transitive dependencies, not all 40. In practice, on the Angular 2 project where
only a subset of RxJS operators are used, the bundle size difference was meaningful.

The migration from Webpack 1 to 2 is not free — the configuration schema changed in a
few places and the loader syntax updated. But Webpack 2 provides a validation layer
that tells you what your config got wrong rather than silently doing the wrong thing,
which is a quality-of-life improvement over the "why is this not working" experience of
Webpack 1. Worth the migration for a greenfield Angular 2 project; I'd wait for final
release before migrating an established Webpack 1 build.
