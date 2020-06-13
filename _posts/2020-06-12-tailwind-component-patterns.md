---
layout: post
title: "Tailwind component patterns without a design system"
date: 2020-06-12
author: marcetux
tags: [css, tailwind, frontend, design]
---
A month of Tailwind on a side project has surfaced the place where utility-first
stops being effortless: the third time you write `flex items-center gap-3 px-4 py-2
rounded text-sm font-medium bg-indigo-600 text-white` for what is clearly a button.
The answer Tailwind gives you is `@apply`, and the way I've learned to use it is
surgical — extract only the things that are genuinely reused across five or more
instances and have a stable semantic meaning.

The pattern: a `components.css` file with a handful of `@layer components` classes
for the things that are truly vocabulary — `btn-primary`, `card`, `input-text`. Not
every repeated element, just the ones where the repetition is semantically
meaningful, not coincidental. The layout utilities — padding, margin, flex, grid —
stay inline in the markup where the specificity is obvious. The semantic components
get classes where naming adds information that `flex-col gap-4` doesn't.

Tailwind's JIT mode (which is excellent; the dev server reflects changes instantly)
makes the `@apply` path more honest — you're not trying to optimize a file size
problem, you're naming something because it has a name worth giving. That's a
different discipline than Bootstrap's "here are components, use them," but it's a
more honest one. You design the vocabulary for your project instead of inheriting
someone else's.
