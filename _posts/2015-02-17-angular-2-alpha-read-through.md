---
layout: post
title: "Reading the Angular 2 alpha"
date: 2015-02-17
author: marcetux
tags: [angular, javascript, frontend, architecture]
---
Angular 2 alpha is available and the change from 1.x is not incremental. Controllers
are gone, replaced entirely by component classes. The scope object is gone. The
dependency injection system is rewritten. It's built with TypeScript and assumes
TypeScript, or at least ES6 with decorator syntax. The reaction in the community has
been pretty loud and mixed, which is understandable — the migration story from 1.x is
essentially "rewrite your app."

What I find interesting is why they made the break. Angular 1 grew from a time when the
web was different: controllers, two-way binding everywhere, and a digest cycle that
worked until it didn't scale. The component model that React pushed into the mainstream
conversation is the right direction, and Angular 2 is essentially adopting it: one-way
data flow by default, components as the unit of composition, explicit change detection.
The TypeScript angle is also honest — the framework uses decorators to declare
components and injected dependencies, and those decorators need types to function
cleanly.

We're using Angular 1.3 in production today and it'll stay there. But I'm taking the
alpha seriously as a preview of where front-end architecture is heading: components,
explicit data flow, typed contracts between pieces. Those ideas aren't going anywhere
regardless of which framework wins.
