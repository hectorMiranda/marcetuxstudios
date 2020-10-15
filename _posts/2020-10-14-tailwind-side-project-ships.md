---
layout: post
title: "The Tailwind side project shipped and what I learned"
date: 2020-10-14
author: marcetux
tags: [css, tailwind, frontend, tooling]
---
The side project I've been building since April — a personal energy-tracking tool
that visualizes the solar and consumption data from the home lab — shipped to
a small group of family and neighbors who have similar setups. It's not polished,
but it works and it looks like I intended it to, which is more than I can say for
most of my solo front-end efforts.

Three months of Tailwind has settled my opinion. The early resistance to long class
strings is just unfamiliarity — after a few weeks you read `flex items-center gap-3`
faster than you read `.header-nav-container` because the former tells you what it does.
The JIT mode (development, instant refresh) combined with the aggressive production
purge keeps the bundle under 15 KB of CSS for a moderately complex app. That's
smaller than the Bootstrap CSS alone, which still includes the grid, typography,
and every component whether you use them or not.

The lesson I'm taking into work: Tailwind is not a replacement for a design system;
it's a language for building one. The `@apply` components I extracted over the project
are a design system — button styles, card styles, form input styles — expressed in
utility classes rather than custom CSS. For a team with a designer, that layer is
already designed. For a solo project, Tailwind means you design it yourself without
starting from nothing. Both uses are legitimate.
