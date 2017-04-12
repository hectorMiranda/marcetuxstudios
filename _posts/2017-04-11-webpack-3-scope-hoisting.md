---
layout: post
title: "Webpack 3 scope hoisting and why the bundle got smaller"
date: 2017-04-11
author: marcetux
tags: [webpack, javascript, performance, frontend]
---
Webpack 3 shipped in beta this month and I pulled it into the dashboard project to see
what changed. The headline feature is scope hoisting — `ModuleConcatenationPlugin` in
the config — and the effect on bundle size was more noticeable than I expected without
changing a line of application code.

The way Webpack 2 works: every module is wrapped in its own factory function, so the
browser has to call a lot of tiny functions to set up the module graph at startup. Scope
hoisting flattens modules that are only used once into the scope that uses them —
inlining the code rather than wrapping it in a function. Fewer function calls, smaller
output, faster startup. The catch is it only works for ES2015 `import`/`export` —
CommonJS `require()` is opaque to the static analysis and gets left as-is. If your
code or a heavy dependency uses `require()`, that part stays wrapped.

For our Angular app, which is written in TypeScript that emits ES modules, the savings
were about 12% on the vendor bundle and 8% on the app bundle — real numbers for a
production asset. The flag to enable it in Webpack 3 is a single plugin entry; I'll
leave a note in examples. The advice: audit your dependencies for CommonJS imports if
you want maximum benefit. The tool tells you which modules it couldn't concatenate if
you run it with `--display-optimization-bailout`.
