---
layout: post
title: "grunt-newer and only rebuilding what changed"
date: 2013-07-13
author: marcetux
tags: [javascript, grunt, tooling, frontend, build]
---
The watcher setup from March runs the full Grunt task on any file change. For a small
project that's fine; as the TypeScript compile step landed and the SASS grew and the
number of image assets increased, the "fast" feedback loop started taking six to eight
seconds per save. Not terrible, but noticeable. `grunt-newer` cut it back to one to
two seconds.

The plugin wraps any Grunt task with timestamp tracking. It checks which source files
have a newer modification time than their output file and runs the task only on that
subset. Save one TypeScript file and only that file gets compiled; the rest of the
TypeScript output is already up to date. SASS compiles incrementally when only one
partial changes. The `concat` and `uglify` steps still run in full because their output
is a single file — can't partially update a bundle — but the compile steps are now
truly incremental.

The one gotcha is the order of task dependencies. If a SASS partial changes, the
partial's containing file needs to recompile even if the parent file itself hasn't
changed. `grunt-newer` has a `newer-then` option for declaring those cross-file
dependencies, and it takes a few minutes to set up correctly. Worth it: the fast loop
is the environment that makes small, safe changes feel natural. Every second you save
per change compounds across a day of iterations.
