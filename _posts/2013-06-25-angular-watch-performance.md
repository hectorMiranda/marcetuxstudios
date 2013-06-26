---
layout: post
title: "AngularJS watch count and the dirty-check budget"
date: 2013-06-25
author: marcetux
tags: [javascript, angular, performance, frontend]
---
The bandwidth dashboard had started to feel sluggish on a moderately fast machine — not
broken, just a noticeable 80ms jank on updates. Chrome's timeline showed the JavaScript
frame taking 70% of that time. The culprit was Angular's digest cycle, and specifically
that I had accumulated around 1,800 active watchers on the main dashboard view.

Angular's dirty-checking model runs a digest loop after every event, comparing the
current and previous value of every watched expression. It's O(n) in the number of
watchers and runs multiple times per event to detect cascading changes. 1,800 watchers
is on the edge of noticeable; 5,000 is visibly slow on anything less than a modern
desktop. The Batarang Chrome extension shows the watcher count by scope; I installed
it, looked at the dashboard, and the number explained the jank immediately.

The fixes were pragmatic: use one-time bindings (`::value`) for expressions that won't
change after render — the customer name, the tier label, the dates on a report row.
Every `::` binding drops out of the watch list after its first non-null value. I also
found a `ng-repeat` over a 200-item list that was repeating without `track by`,
generating hundreds of implicit watchers — `track by customer.id` halves the work by
reusing DOM elements instead of destroying and recreating them. Watcher count dropped
to 400. The jank disappeared. You can't spend what you don't have.
