---
layout: post
title: "Twitter Bootstrap, and not designing from scratch"
date: 2013-02-25
author: marcetux
tags: [css, bootstrap, frontend, design]
---
A new internal tool needed a UI, and I am not a designer. A couple of years ago that
meant hand-rolling CSS that looked like a backend developer made it — because one had.
Twitter Bootstrap is the thing that lets me skip that, and 2.3 landed this month, so
the timing was right.

It's a grid, a set of components, and a sane default look, all driven by LESS — which
slots straight into the build I already have. The grid alone earns it: a responsive
12-column layout that reflows on a narrow screen without me writing a media query,
which matters now that people open these dashboards on tablets. Forms, buttons,
tables, nav — all already styled, all consistent, none of it my problem to invent.

The known risk is the "every site looks like Bootstrap" sameness, and for a customer-
facing product I'd care. For an *internal tool* I care exactly zero — I want it
legible and consistent and shipped, not distinctive. And because it's LESS variables
under the hood, the day it does matter I can reskin the colors and spacing without
forking the framework. Start from a good default, override where it earns its keep.
The blank CSS file was never the noble choice; it was just slower.
