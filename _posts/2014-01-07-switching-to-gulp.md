---
layout: post
title: "Switching from Grunt to Gulp"
date: 2014-01-07
author: marcetux
tags: [javascript, gulp, grunt, tooling, build]
---
The Grunt build that served 2013 well enough has been getting slower. Not catastrophically
slow — three or four seconds — but that's long enough that I stop saving as often, which
is the kind of friction that adds up. A few people I follow have been talking about Gulp,
so I carved out a weekend to port the build and see if the different model is actually
better or just different.

Gulp's pitch is streams. Instead of writing files to disk between every step, it pipes
the contents through a chain of transforms in memory and only touches disk at the end.
The `gulpfile.js` reads more like code and less like a config file, too — it's just
JavaScript calling functions, no `grunt.initConfig` object to remember the shape of. My
TypeScript compile, LESS, concat, and uglify chain got noticeably shorter and the cold
build dropped to under a second.

The one thing Grunt still does better is the plugin ecosystem, which is more mature.
A few niche transforms I was using had no Gulp equivalent and I had to wire them
differently. But for the core front-end build loop — compile, bundle, watch — Gulp is
faster and more readable. I'll keep Grunt around for the deployment tasks that have no
equivalent yet. Pick the tool for the job, not the brand.
