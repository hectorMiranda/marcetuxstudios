---
layout: post
title: "First real project in AngularJS"
date: 2013-01-04
author: marcetux
tags: [javascript, angular, spa, frontend]
---
Built that test screen in AngularJS and I see what the fuss is about. Two-way
binding I already liked from Knockout, but Angular wraps it in a full app structure
— controllers, services, and a dependency-injection system that makes the pieces
testable in isolation.

The thing that clicked: **directives.** A custom `<bandwidth-chart>` element that
encapsulates its own template and behavior is a real component, not a jQuery plugin
bolted onto a div. That's the abstraction Backbone made me build by hand every time.
And `$http` returning promises means the AJAX staircase I complained about in the
Deferreds post just... flattens.

The parts that worry me: the magic is deep, and when binding doesn't update you're
debugging a digest cycle you didn't know existed. And the dependency-injection by
*parameter name* is clever until a minifier renames your parameters and everything
breaks. There's an annotation fix, but it's a sharp edge for newcomers. Net: I'm
sold enough to build the next dashboard in it.
