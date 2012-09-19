---
layout: post
title: "Backbone.js when jQuery isn't enough"
date: 2012-09-18
author: marcetux
tags: [javascript, backbone, frontend, architecture]
---

Yesterday Bootstrap, today the JavaScript side of the same rebuild. Knockout
handles the data-bound tables nicely, but a couple of screens have grown real
client-side *behavior* — routing between views, models that sync with the API —
and that's where I reached for Backbone.

Backbone is barely a framework and that's the appeal. Models wrap your data and
talk to a REST endpoint with `fetch`/`save`. Collections are lists of models that
fire events when they change. Views own a chunk of the DOM and re-render when their
model does. A router maps `#/reports/42` to a function. That's most of it.

The discipline it imposes is the value: **your data lives in models, not scattered
across DOM nodes and closures.** When the bandwidth report updates, the model
changes, an event fires, the view re-renders. No more hunting for the seven places
that touch the same `<span>`.

It's not magic — you write your own rendering (I'm using Underscore templates), and
you feel every line. But for an app that's outgrown "jQuery soup," the structure is
exactly what was missing.
