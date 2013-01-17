---
layout: post
title: "Grunt and a real front-end build"
date: 2013-01-16
author: marcetux
tags: [javascript, grunt, tooling, frontend]
---
The front end has enough moving parts now — LESS to compile, JS to concatenate and
minify, an Angular app to assemble — that doing it by hand or with a pile of MSBuild
hacks stopped scaling. Grunt is the JavaScript-world answer, and keeping the
front-end build in front-end tooling just feels right.

A `Gruntfile.js` declares tasks: compile LESS, concat and uglify the scripts, run
JSHint so a stray syntax error fails the build instead of the browser, and a `watch`
task that re-runs the relevant step the instant I save a file. That watch loop is the
quality-of-life change — save the LESS, the CSS rebuilds before I've switched to the
browser.

It's config-heavy and a little fiddly to set up, and the plugin-for-everything model
means you're trusting a lot of small packages. But having one command that produces
the deployable front-end — and a watch that makes development feel instant — is worth
the afternoon it took to wire up. The Gruntfile's in `examples/`.
