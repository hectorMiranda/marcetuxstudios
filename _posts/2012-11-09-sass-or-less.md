---
layout: post
title: "SASS or LESS, revisited"
date: 2012-11-09
author: marcetux
tags: [css, sass, less, frontend]
---
I wrote about LESS last month because Bootstrap ships in it. A few weeks of real use
later, I keep eyeing SASS, and it's worth saying why without starting a holy war.

They solve the same problem — variables, nesting, mixins for CSS — and 90% of the
syntax rhymes. The differences are at the edges: SASS (the SCSS flavor) has stronger
logic — real conditionals, loops, functions — which matters once your stylesheet
starts *computing* things like a color palette or a spacing scale. LESS keeps it
lighter and does its compilation story differently.

My honest take: if you're deep in a LESS ecosystem (Bootstrap), stay; the switching
cost isn't worth it. Starting fresh, I'd pick SCSS for the extra power and the
tooling momentum. Either way the win is the same — stop hand-maintaining repetitive
CSS. The preprocessor is the point, not which one.
