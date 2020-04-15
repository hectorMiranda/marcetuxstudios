---
layout: post
title: "Tailwind and the utility-first argument"
date: 2020-04-14
author: marcetux
tags: [css, tailwind, frontend, tooling]
---
I've been skeptical of utility-first CSS since the argument is essentially "class
names in your HTML are fine, actually," which reads as a regression to the inline
styles debate from a decade ago. But enough people whose front-end instincts I
trust were shipping with Tailwind that I spent a weekend on a side project with it
instead of my usual Bootstrap setup.

The reframe that made it click: Tailwind isn't "no CSS," it's "no naming." The hard
part of writing CSS for components isn't the properties; it's inventing a meaningful
name for every selector and then maintaining the mental map between the name and
the actual styles. `.card-header-inner-title-container` tells you nothing useful.
Tailwind trades that naming problem for composition in the markup — `flex items-center
gap-4 text-sm font-medium text-gray-700` tells you exactly what the element looks
like without opening a stylesheet.

The complaint about long class strings is real but manageable — Tailwind's `@apply`
lets you extract repeated patterns back into a class when repetition earns naming.
The purge step (which removes unused utilities from the production bundle) keeps the
output lean. I'm not switching the bank's internal tools off Bootstrap anytime soon —
that's a team decision and Bootstrap is fine — but for personal projects and anything
I start fresh, Tailwind's answer to the naming problem is better than the one I
had before.
