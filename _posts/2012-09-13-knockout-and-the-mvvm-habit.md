---
layout: post
title: "Knockout.js and the MVVM habit"
date: 2012-09-13
author: marcetux
tags: [javascript, knockout, mvvm, frontend]
---

The reporting dashboards have crept into "too much jQuery" territory — the kind of
page where you're hand-syncing DOM nodes with data and losing track of which is the
source of truth. I've been pulling Knockout.js in to fix exactly that.

Knockout's pitch is MVVM in the browser: you make a **view model** of plain
observables, bind DOM elements to them with `data-bind` attributes, and stop
touching the DOM by hand. Change an observable and every binding that depends on it
updates itself. Computed observables let derived values (totals, filtered lists)
recalculate automatically.

The mental shift is the same one MVVM taught me on the desktop years ago: the view
is a *projection* of state, not a place you store state. Once the bandwidth numbers
live in observables, the table, the summary line, and the little "last updated"
label all just track them.

It's not a whole framework and I like that. It does data binding and gets out of
the way. For pages that are mostly "show this server data and let the user poke at
it," that's the right amount of tool.
