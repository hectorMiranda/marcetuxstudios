---
layout: post
title: "Angular 2 beta and the new router"
date: 2016-03-18
author: marcetux
tags: [angular, javascript, frontend, spa, typescript]
---
Angular 2 has been in beta since December and the team has been moving fast enough
that "beta" means something different than it used to. The core is stable; the router
has broken its API twice. I spent a few evenings this month porting a side project from
the initial beta to the current one just to stay calibrated on where it's heading, not
because the production app is going there yet.

The component model is genuinely better than Angular 1's controller/scope/directive
tangle. A component is a class with a decorator and a template — no scope inheritance,
no `$apply`, no `$digest` anxiety. TypeScript is first-class: the Angular team uses it
internally, the type definitions are accurate, and the tooling around it is the best
TypeScript experience I've had in a web framework. The dependency injection system is
familiar enough from Angular 1 to transfer, different enough in implementation that
you'll hit surprises.

The router situation is the thing I'm watching. The component router introduced in beta
replaced the one from earlier betas, and there's already talk of another revision. A
router is not something you want to refactor mid-project. I'll use Angular 2 for new
things when the router is stable and preferably boring. The core is ready. The
surrounding pieces need one more quarter.
