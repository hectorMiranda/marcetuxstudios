---
layout: post
title: "Slimming down Angular controllers"
date: 2014-01-21
author: marcetux
tags: [javascript, angular, architecture, frontend]
---
The customer-detail controller grew past three hundred lines over Q4 and I've been
avoiding it. This week I finally cut it down, and the refactor was a reminder of a
rule I keep relearning: if a controller knows how the data is shaped and how the HTTP
call is made *and* what to do when the user clicks, it's doing three jobs.

The fix I applied is the same one from the services post last year: anything that
touches the server lives in a service, anything that formats or transforms data lives in
a filter or a separate helper, and the controller is left with just binding values and
reacting to user events. The three-hundred-line controller shrank to about sixty. The
services are now independently testable — I can hand them a mock `$http` and verify
they parse the response correctly without touching the DOM.

The thing that helped most was the Angular style guide that's circulating — John
Papa's take on how AngularJS 1.x apps should be structured. Not all of it applies to
a project that already has a shape, but the "one thing per file" rule and the explicit
`controllerAs` pattern are worth adopting now, before the next controller balloons.
Structure doesn't write itself; it has to be tended.
