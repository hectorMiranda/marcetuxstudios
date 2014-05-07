---
layout: post
title: "Gulp sourcemaps and incremental watching"
date: 2014-05-06
author: marcetux
tags: [javascript, gulp, tooling, sourcemaps, build]
---
The Gulp build from January got a second pass this month because the watch task was
rebuilding everything on every save, which defeated the point of fast incremental builds.
A TypeScript compile that touches all files when one changes isn't much faster than a
Grunt build — just louder about it.

The fix was `gulp-cached` paired with `gulp-remember`. `gulp-cached` passes only files
that changed since the last run through the pipeline; `gulp-remember` reunites the cached
output of unchanged files with the freshly compiled changed ones before concat. The
combination means that a change to one TypeScript file compiles only that file and
re-concatenates the bundle — the rest of the compile step is skipped. Watch cycle went
from three seconds to under half a second.

The sourcemaps piece was wiring `gulp-sourcemaps` through the TypeScript and uglify steps
so maps chain correctly — a breakpoint in the minified bundle traces back to the original
`.ts` file. The magic is calling `sourcemaps.init()` before the compile steps and
`sourcemaps.write('.')` at the end to emit the `.map` sidecar. The chain works; the
DevTools debugger shows original TypeScript in the source panel while the browser runs
the minified bundle. This is the full cycle from the source-maps post eighteen months
ago, now actually wired end to end.
