---
layout: post
title: "Angular directives as reusable dashboard widgets"
date: 2013-05-17
author: marcetux
tags: [javascript, angular, spa, frontend, directives]
---
The bandwidth chart appeared in three different views, each with its own copy of
the D3 code and the data-fetching logic. When design asked to add a unit toggle to the
chart — megabits versus gigabits — I updated one copy, forgot the other two, and shipped
the inconsistency. The fix was to move the chart into an Angular directive and have
the three views share a single component.

A directive is Angular's mechanism for extending HTML with custom elements and
behaviors. `<bandwidth-chart customer-id="current.id">` is now a real tag in my
templates, and the directive definition handles the D3 rendering, the unit toggle, and
the data fetch through its own isolated scope. The parent controller gives it the
customer ID and stops caring about how bandwidth is displayed.

The isolated scope is the part that matters for reuse: the directive doesn't reach into
the parent scope's variables by name, so it can be dropped anywhere without worrying
about name collisions. The parent binds explicitly: `customer-id` maps to an attribute
binding, the directive reads `@customerId`, and there's a clear, declared contract
between the widget and its caller. The three views now render from the same directive,
the unit toggle is in one place, and the D3 code I don't fully understand lives in
exactly one file.
