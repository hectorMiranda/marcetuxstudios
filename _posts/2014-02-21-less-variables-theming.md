---
layout: post
title: "LESS variables and theming without forking"
date: 2014-02-21
author: marcetux
tags: [css, less, frontend, design, tooling]
---
The portal got a rebrand request this month — different logo, different primary color,
slightly adjusted typography. In the old days that would have meant a grep through CSS
for hex codes and a week of regression checks. With LESS, it was a twenty-minute
afternoon that I felt vaguely smug about.

The setup that makes this work is a `_variables.less` file that sits at the top of the
import chain. Brand colors, font families, base spacing, breakpoints — all defined once
as variables. The rest of the CSS references `@primary-color` and `@font-body` without
any hardcoded values. Changing the theme is changing the variables file; every
component that references those variables picks up the change on the next build.

The Bootstrap integration fits naturally: Bootstrap's own LESS source exposes the same
kind of variable file, so overriding `@link-color` before importing Bootstrap reskins
all the Bootstrap components in the same pass. I did have to track down a handful of
places that had hardcoded hex codes — old CSS that predates the LESS migration — but
the search was short. The discipline of "never put a color in a rule, always use a
variable" makes refactoring feel like editing a config file.
