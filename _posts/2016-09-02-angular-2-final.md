---
layout: post
title: "Angular 2 is final and the wait was worth it"
date: 2016-09-02
author: marcetux
tags: [angular, javascript, frontend, typescript, spa]
---
Angular 2 shipped final on September 14th and I had the internal tool on the release
version by the 16th — the RC-to-final migration was under an hour because the RC was
already stable and the final release broke nothing. The version number drama is real
(Angular 2 becomes Angular, Angular 1 becomes AngularJS, future versions will be
Angular 4 then Angular 5 following semver on the router package) but it doesn't change
what the framework does.

The final product is what I expected from the RCs: a component-based framework with
TypeScript first-class, a competent router, RxJS integrated for async data handling,
and a dependency injection system that's familiar without being identical to the Angular
1 version. The change detection strategy — the ability to mark a component as
`ChangeDetectionStrategy.OnPush` and have it only re-render when inputs change — is
the kind of performance knob that Angular 1 made difficult and Angular 2 makes obvious.

The thing I want to say clearly for the "should I switch" conversation: Angular 2 and
AngularJS are different frameworks that share a name. The learning curve is real. But
if you're starting a new project and TypeScript is your language, Angular 2 is a
defensible choice that won't feel like a bet on the underdog. The framework is done;
the remaining question is tooling maturity and the Webpack 2 story, both of which are
in good shape.
