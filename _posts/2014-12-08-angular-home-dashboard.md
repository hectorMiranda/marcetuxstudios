---
layout: post
title: "A real frontend for the home sensor dashboard"
date: 2014-12-08
author: marcetux
tags: [javascript, angular, iot, d3, frontend, maker]
---
The gnuplot graphs on the Raspberry Pi served their purpose — they proved that the data
collection worked and that the data was interesting enough to keep collecting. This
weekend I replaced them with an Angular frontend that queries the Sinatra API and
renders the readings with D3.js. The dashboard is now something I'd be comfortable
showing someone.

The D3 integration inside Angular takes a specific pattern. D3 manipulates the DOM
directly — that's its whole model — and Angular prefers you to touch the DOM only inside
directives. So the chart is a directive: the `link` function receives the element and the
scope's data, calls into D3 to draw the SVG, and listens for scope changes to redraw.
Angular handles the data fetching and binding; D3 handles the drawing. They don't
step on each other as long as the boundary is clear.

The dashboard shows three temperature readings on a time series chart that auto-updates
every sixty seconds via Angular's `$interval`, a door event log for the last twenty-four
hours, and a summary table. The Raspberry Pi is generating the HTML from Angular's
`$http` calls to its own Sinatra API. The data is four months old now in the earliest
records. Seeing that temperature graph going back to August, knowing I built the thing
that collected it, is a different kind of satisfaction than shipping product features.
