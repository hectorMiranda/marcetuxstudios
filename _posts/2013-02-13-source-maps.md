---
layout: post
title: "Source maps make minified JS debuggable again"
date: 2013-02-13
author: marcetux
tags: [javascript, debugging, tooling, frontend]
---
The Grunt build from January gave me a single minified `app.min.js`, which is great
for users and miserable for me the moment something throws on line 1, column 24,801.
Source maps are the fix I'd been ignoring, and turning them on felt like switching
the lights back on.

A source map is a sidecar file that records how the minified output maps back to your
original files. The browser's dev tools read it, and suddenly the debugger shows my
*real* source — original variable names, original line numbers — while the page is
running the minified bundle. Set a breakpoint in the file I actually wrote; step
through code that doesn't exist on the server in that form. It's a small bit of
plumbing for an enormous quality-of-life gain.

The nice part is it composes with everything else: the uglify step emits a map, and
once I'm compiling TypeScript that map can chain all the way back through the minified
JS to the original `.ts`. The discipline is don't ship the `.map` to users who don't
need it, but make sure it's there for me. Minify for them, debug as if you didn't —
that's the deal source maps quietly make.
