---
layout: post
title: "ES2015 modules replacing the CommonJS habit"
date: 2015-06-04
author: marcetux
tags: [javascript, es2015, modules, frontend, webpack]
---
The matching module was already split into separate files — one per service, one per
utility — connected by `require()`. Migrating to ES2015 `import`/`export` syntax was
straightforward and the output through Babel is functionally identical, but the syntax
changes the feel of the code in a way I didn't expect.

`export` is a declaration-level annotation: `export class ProfileFilter` or `export
function matches()` — the export is part of the definition, not a separate assignment at
the end of the file. `import` at the top of every consumer file is the explicit
dependency list for that module. The contrast with CommonJS's `require()` anywhere in
the file, returning anything: ES2015 modules are static. The bundler — Webpack in this
case — can analyze the imports without running the code, which enables tree shaking once
Webpack 2 ships. Dead code, eliminated at bundle time.

The named export pattern has also improved code review. With `module.exports = { a, b,
c }` at the end of a CommonJS file, you have to read to the end to know what's public.
With `export` on each declaration, the public surface is visible in-line. The import
site tells you exactly what it depends on. That information was always there; now it's
where you look first instead of where you look last.
