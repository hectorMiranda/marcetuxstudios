---
layout: post
title: "Grunt watch and the fast feedback loop"
date: 2013-03-19
author: marcetux
tags: [javascript, grunt, tooling, frontend, workflow]
---
The Grunt build from January runs on demand, which was fine for the "build before you
push" habit but awful the moment I was making a lot of small CSS tweaks and hitting
F5 every thirty seconds. Added `grunt-contrib-watch` this week and the feedback loop
compressed by something like a factor of ten.

The watcher monitors a glob pattern — `src/styles/**/*.less`, `src/js/**/*.js` — and
reruns only the relevant tasks when a file changes. Save a LESS file and in two seconds
the styles are recompiled and the browser can pick them up on refresh. No full build,
no coffee break, just the minimum work to propagate the change. The first time it
catches a compile error and shows it in the terminal while I still have the file open,
instead of after I've tabbed away to something else, feels like a completely different
way of working.

LiveReload is the next step — the watcher signals the browser to reload automatically —
but the watcher alone already changed the rhythm enough that I'm not chasing it today.
The lesson for me is how much a slow build loop conditions you to make bigger,
riskier changes per save to justify the wait. Short the loop, and small, safe
iterations become the natural move.
