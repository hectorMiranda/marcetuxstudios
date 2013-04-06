---
layout: post
title: "Switching from LESS to SASS and why I probably should have done it first"
date: 2013-04-06
author: marcetux
tags: [css, sass, frontend, tooling, build]
---
The Bootstrap integration from February runs on LESS because that's what Bootstrap 2.x
ships. A new internal tool starting from scratch gave me the opportunity to try SASS
instead, and after a few days I'm not going back for anything where I have control
of the choice.

Both do the same fundamental things — variables, nesting, mixins, imports — but SASS
(specifically the SCSS syntax, which is just CSS with superpowers) does them with a bit
more muscle. The `@extend` directive lets a class silently inherit all the rules of
another without generating a pile of selectors in the output; `@each` lets you loop over
a list to generate repetitive rules without repetitive code; `@function` returns a
computed value. These aren't features I reach for every file, but when I need them LESS
makes me work around them. SASS has them.

The practical friction is the Ruby dependency — `sass` is a gem — but that's in the
Vagrant box already and the Grunt plugin wraps it cleanly. Compile time is a few hundred
milliseconds on our size of styles, which the watcher absorbs. For Bootstrap's own
variables and grid I'll keep the LESS source as long as Bootstrap ships LESS, but
anything I own goes SASS from here.
