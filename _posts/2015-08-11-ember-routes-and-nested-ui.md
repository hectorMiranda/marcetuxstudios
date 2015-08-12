---
layout: post
title: "Ember routes and nested UI state"
date: 2015-08-11
author: marcetux
tags: [ember, javascript, frontend, routing, spa]
---
The JibJab member area has a classic nested UI: a top-level library view, a sidebar
showing categories, and a main area showing the selected category's videos. Multiple
parts of the URL map to multiple levels of the UI simultaneously. Ember's nested route
model is built for exactly this shape and once I stopped fighting it and accepted the
convention, the state management almost disappeared.

Ember's router maps URL segments to routes arranged in a tree. The outlet in each
route's template is where the child route renders. So `library` renders the outer chrome
including the sidebar, and `library/category/:id` renders the main area *inside* that
same chrome through the `{{outlet}}`. Navigating between categories re-renders only the
main area, not the sidebar and outer chrome. The URL is the state, the router is the
source of truth, and the component tree reflects both.

The part that took the most adjustment from an Angular background: Ember routes have
lifecycle hooks — `model()`, `afterModel()`, `setupController()` — that happen before
the template renders. The data fetching happens in the route, not in the component.
Components receive data and display it; routes are responsible for having the data ready.
It's a cleaner separation than I'd have reached for intuitively, and the result is that
components are genuinely reusable because they don't know where their data came from.
