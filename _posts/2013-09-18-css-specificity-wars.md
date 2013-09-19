---
layout: post
title: "CSS specificity wars and the discipline of flat selectors"
date: 2013-09-18
author: marcetux
tags: [css, sass, frontend, architecture]
---
The Bootstrap-based styles for the customer portal have accumulated eighteen months of
overrides — rules added when something "wasn't quite right" — and some of them are now
fighting each other. The symptom is the classic: add a rule that should change an
element, nothing changes, add `!important`, it changes, next sprint a different rule
stops working. The root cause is specificity creep: selectors that got more specific
to win a battle ended up claiming territory their author didn't intend.

CSS specificity is a tiebreaker: given two rules that match an element, the one with
higher specificity wins, regardless of source order. An ID selector beats any number
of class selectors; a class selector beats any number of element selectors. Nesting
your selectors — `.dashboard .widget ul li a` — doesn't just style a specific element,
it creates a specificity debt that any rule targeting `a` in any other context has to
beat.

The SASS refactor I did this week applies one discipline: no selector should be deeper
than three levels, and IDs are for JavaScript hooks only, never for styling. Classes
style; IDs identify. Flat selectors mean specificity is predictable — class beats element,
and the source order tiebreaker is readable. It's not always possible to be this flat
with Bootstrap's existing markup in the mix, but enforcing it on new components and
slowly refactoring the worst existing offenders changed the stylesheet from a war zone
to something I can reason about again.
