---
layout: post
title: "Angular 2 RC and the router is finally stable"
date: 2016-07-07
author: marcetux
tags: [angular, javascript, frontend, spa, typescript]
---
Angular 2 has been in release-candidate status since May and the team finally landed the
router revision I've been waiting for. The previous two router implementations each
broke in ways that made adoption feel risky; the RC router — which is the one that will
ship with the final release — has a stable API and documentation that matches the actual
behavior. I spent a weekend building a medium-sized prototype to test it properly.

The routing model is decorator-based: you annotate a component with `@RouteConfig` (or
configure routes in the root module) and the router handles navigation, history, and
parameter extraction. Lazy loading is supported as a first-class feature — you can
split a large app into route-level chunks that load on demand rather than sending a
monolithic bundle. That's something Angular 1 never had without a lot of creativity.

My honest verdict: Angular 2 RC is ready for a new project today. The core is stable,
the router is stable, and TypeScript integration is the best in class among JS
frameworks. The final release should arrive before summer ends based on the cadence.
I'm planning a new internal tool in Angular 2 rather than Angular 1 — the component
model is genuinely better, and starting a new project in a framework with a known
end-of-life on its deprecation clock is not something I want to repeat.
