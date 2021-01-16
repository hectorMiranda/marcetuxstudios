---
layout: post
title: "Tailwind CSS at arm's length"
date: 2021-01-15
author: marcetux
tags: [css, tailwind, frontend, design]
---
A team member dropped Tailwind into a prototype without asking, and instead of
having the argument in a PR I spent a weekend actually using it. The utility-first
premise annoyed me in principle — isn't that just inline styles with extra steps?
— and I wanted to have an informed opinion before reverting their branch.

The click for me came about three hours in: the constraint is the feature. You're
not writing arbitrary values; you're picking from a design scale that someone
thought through — spacing in multiples of four, a type scale, a consistent color
palette. The result is that two different developers independently choosing
`px-4 py-2 rounded` will get the same spacing instead of two different gut-feel
values in two separate class names. The variance collapses, which is the thing
component systems try and often fail to achieve through convention.

I still think the HTML gets noisy and I'd pull shared patterns into components
rather than repeating seventeen class names in a template. The prototype, though,
was readable once I stopped expecting it to look like LESS. The PR didn't get
reverted. It got merged with a note about extracting the card variant into an
`@apply` rule so we're not copying that cluster of classes everywhere. Informed
opinions are more useful than aesthetic ones.
