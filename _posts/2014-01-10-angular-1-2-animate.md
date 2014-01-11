---
layout: post
title: "AngularJS 1.2 and ngAnimate"
date: 2014-01-10
author: marcetux
tags: [javascript, angular, frontend, animation, css]
---
The dashboards upgraded to AngularJS 1.2 over the break, which is mostly a stability
pass on what we were already running. But `ngAnimate` is new enough to warrant a look,
because the old way of handling enter/leave transitions was a pile of jQuery toggle calls
that belonged nowhere near a controller.

The mechanism is CSS classes. Angular now adds classes like `ng-enter` and `ng-enter-active`
around lifecycle hooks — when an element appears, when it leaves, when it's repeated. You
hook into those with a CSS transition or animation, and the framework handles the timing.
The template stops having to know about jQuery and the CSS owns the motion, which is the
right division. A dashboard panel that fades in when its data resolves now does so entirely
in two class rules.

The caution is that `ngAnimate` is opt-in by importing `ngAnimate` as a dependency, so
upgrading doesn't break apps that don't want it. That was the right call — I turned it on
for new panels only and the existing views were untouched. Animations are easy to overdo
on a data-dense dashboard, so I'm keeping them minimal: a quick fade on data load, nothing
that calls attention to itself. Motion should communicate state, not decorate.
