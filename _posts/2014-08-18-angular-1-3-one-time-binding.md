---
layout: post
title: "AngularJS 1.3 beta and one-time binding"
date: 2014-08-18
author: marcetux
tags: [javascript, angular, frontend, performance]
---
AngularJS 1.3 is in beta and the feature I was looking forward to is one-time binding.
The double-colon syntax — `{{ ::expression }}` — tells the digest cycle to evaluate the
expression once, set the DOM, and stop watching it. For a dashboard with static labels
and display-once data, this trims the watcher count significantly.

The watcher count is the real performance lever in Angular 1.x applications. Every
`{{ expression }}`, every `ng-if`, every `ng-show` adds a watcher that the digest cycle
checks on every cycle. A view with hundreds of bindings runs the digest cycle over all
of them even when nothing changed. The result is a visible lag when the UI is busy
updating other parts of the view — the whole cycle runs before the browser can paint.

In the reporting dashboard, about a third of the displayed values are loaded once and
never change: customer names, plan labels, report header data. Switching those to
one-time bindings with `::` dropped the watcher count from around 400 to about 280.
The digest cycle got measurably faster in the Chrome profiler — frame time on a busy
update went from 20ms to 12ms. The one-time binding is the cheapest performance
optimization Angular 1.3 offers, and you can apply it incrementally without changing
the view structure.
