---
layout: post
title: "Building the year in a stack"
date: 2013-10-21
author: marcetux
tags: [architecture, dotnet, angular, retrospective, engineering]
---
Nine months in, the pieces have a shape. The front end is Angular 1.x with UI-Router
for nested views, TypeScript for the service layer, SASS for styles, and Gulp for the
build. Tests are Jasmine with ngMock for services and Protractor for E2E. The back end
is Web API 2 on OWIN/Katana, with HMAC signing for server-to-server API clients and
OAuth2 Authorization Code for the browser-based portal. Redis handles sessions, the
SignalR backplane, and configuration-change pub/sub. SQL Server on the data tier with
async EF queries and careful query plan work.

What makes it feel like a stack rather than a pile is that each piece fits a defined
seam. Angular's isolated-scope directives and explicit `$http` services stop the
front end from coupling to URL shapes. Web API controllers are thin because the service
layer carries the domain logic and the DI container makes both testable in isolation.
OWIN middleware handles cross-cutting concerns — timing, auth, CORS — in one ordered
pipeline. Redis keeps state out of web server memory, which is what lets the two-server
setup work.

The gaps I know about: no queue-driven async processing for the heavy report generation
(it's synchronous today and it will bite us as the dataset grows), no distributed
tracing, and the E2E test coverage is six happy-path flows. The stack is solid for
the load it's carrying; I know what breaks first when the load triples.
