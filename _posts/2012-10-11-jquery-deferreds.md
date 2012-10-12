---
layout: post
title: "jQuery Deferreds, or promises before promises"
date: 2012-10-11
author: marcetux
tags: [javascript, jquery, async, promises]
---

The browser side of this week's work had the same disease the C# side had before
`await`: nested callbacks. Three AJAX calls that depend on each other turn into a
staircase drifting off the right edge of the editor. jQuery's Deferreds are the
cure I reached for.

A `$.ajax` call returns a promise-ish object. Instead of passing a `success`
callback, you chain `.done()`, `.fail()`, and `.then()`. The real power is
composition: `$.when(a, b, c)` waits for several calls and fires once when all
resolve, so "load the report, the user prefs, and the labels, then render" stops
being a counter variable and three nested callbacks.

It's not the standardized Promise we'll eventually get in the language — the
semantics around `then` chaining and error propagation are a little quirky — but in
2012 it's the pragmatic tool that's already in every project that uses jQuery.

The same lesson as the server: flatten the staircase. Whether it's `await` or
`.then()`, the win is code that reads top-to-bottom in the order things happen.
