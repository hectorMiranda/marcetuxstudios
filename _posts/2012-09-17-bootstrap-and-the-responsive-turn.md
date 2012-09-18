---
layout: post
title: "Bootstrap and the responsive turn"
date: 2012-09-17
author: marcetux
tags: [css, bootstrap, responsive, frontend]
---

I'm rebuilding a portal's layout on Bootstrap, and the thing that's reorganizing my
brain isn't the pretty buttons — it's the **grid and the media queries underneath
it.**

For years "the site" meant a fixed 960px column and a separate, sad m-dot mobile
site nobody maintained. Bootstrap's 12-column responsive grid finally makes one
layout that reflows: rows of columns that stack on narrow screens because the CSS
says so, not because you built a second site.

What I appreciate as a backend-leaning engineer is that it's *conventions*. Drop in
`.container`, `.row`, `.span6`, and you get something coherent without inventing a
CSS architecture from scratch on every project. The default components — navbars,
tables, forms — look deliberate, which for an internal reporting tool is already a
step up from "developer art."

The trap I'm watching for: every Bootstrap site looks like every other Bootstrap
site. For a customer-facing product that matters; for an internal dashboard where
the job is "show the numbers clearly," I'll happily take the convention and spend
my creativity on the data instead of the chrome.
