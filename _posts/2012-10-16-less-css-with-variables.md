---
layout: post
title: "LESS: CSS that finally has variables"
date: 2012-10-16
author: marcetux
tags: [css, less, frontend, tooling]
---

Bootstrap is built in LESS, and once I cracked it open to retheme our portal I
didn't want to go back to plain CSS. The pitch is simple: the stylesheet language
we've all suffered finally gets **variables, nesting, and mixins.**

`@brand-color: #2a6f97;` once, used everywhere — change the brand in one place
instead of find-replacing a hex code across a dozen files. Nesting lets related
rules live together instead of repeating selectors. Mixins are reusable chunks of
declarations, so the rounded-corner incantation is written once and called by name.

The thing to keep straight: LESS compiles *to* CSS. There's no magic at runtime
(well — you can compile in the browser for development, but you ship the compiled
CSS). It's a preprocessor, a build step, the same mental model as minification.

My one rule so far: don't over-nest. It's tempting to mirror your whole DOM tree in
nested selectors and end up with specificity wars and selectors a mile long. Nest a
couple of levels for grouping, then stop.
