---
layout: post
title: "Writing Angular directives for the first time"
date: 2014-03-10
author: marcetux
tags: [javascript, angular, directives, frontend, components]
---
I'd been avoiding writing custom directives for months, satisfied with the ones that
ship with Angular and the UI-Bootstrap suite. Then I needed a bandwidth sparkline that
appeared in five different places with slightly different axis labels and color thresholds,
and the copy-paste-and-adjust approach failed on the third copy. Directive time.

An Angular directive is a way to teach the HTML new elements or attributes that carry
behavior. The `link` function gets the element and the scope and can touch the DOM
directly — which is the only place in an Angular app where direct DOM access is
acceptable. The `scope: { ... }` definition in the directive config isolates the
directive's scope from its parent, so each instance of the sparkline has its own data
and threshold without sharing state. This is the piece I'd misunderstood from reading
about directives: isolated scope isn't a limitation, it's what makes the directive
reusable.

The sparkline directive ended up taking a data array and a threshold value as attributes
and using a small SVG path element to draw the line. Each of the five call sites passes
different data; the directive doesn't know or care. The template got shorter, the
repetition went away, and I have a component I can drop into any view. I should have
written directives sooner — the mental model took a day to click and now feels obvious.
