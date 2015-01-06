---
layout: post
title: "ES6 classes with Babel, not waiting for browsers"
date: 2015-01-05
author: marcetux
tags: [javascript, es6, babel, frontend, tooling]
---
The spec isn't final yet but the interesting parts of ES6 have been stable enough to
use for months — you just need a transpiler step between your source and the browser.
I wired Babel into the Gulp pipeline this week and started writing `class` syntax for
the profile matching module, and the experience is about as boring as it should be.

Babel takes your ES6 and emits ES5 that every browser we support can run. Classes
desugar to the prototype pattern you'd write by hand, arrow functions desugar to
`function` plus a captured `this` — nothing magic, nothing you couldn't inspect in the
output. The value is that the source is easier to read and maintain, not that browsers
get new powers. The `class` keyword especially helps when onboarding someone from a
Java or C# background: there's a familiar anchor even if the inheritance model is still
prototypal underneath.

What surprised me is how little the pipeline changed. A Babel transform step in Gulp,
the right preset for the ES6 features we're using, and the source maps that already
existed chain through correctly. The output is still just a minified bundle. Start
using the language; don't wait for the platform to catch up to you.
