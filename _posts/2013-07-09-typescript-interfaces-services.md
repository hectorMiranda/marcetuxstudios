---
layout: post
title: "TypeScript interfaces as service contracts across the Angular codebase"
date: 2013-07-09
author: marcetux
tags: [typescript, javascript, angular, frontend, architecture]
---
The TypeScript trial from February has spread to the full Angular codebase, not just
the service layer I started with. The thing that made the expansion feel worthwhile
wasn't the compile-time property checks, which are already delivering — it's the
interfaces for the API response shapes.

The convention I settled on is a file of `interface` declarations that mirror each API
resource: `ICustomer`, `IBandwidthSample`, `IReportRow`. Every Angular service that
calls an endpoint is typed to return `ng.IPromise<ICustomer[]>` or similar. The
controllers that call those services are typed to receive those shapes. TypeScript checks
that every path from API call to template binding is using a consistent type — if I
rename a field on the server and forget to update the interface, the interface update
shows me everywhere the old name was used.

The ergonomics in a TypeScript 0.9 world are still a little rough — the AngularJS
type definitions are community-maintained and I've patched them twice — but the
definition file discipline pays forward. When a new team member reads the `ICustomer`
interface, they know exactly what fields the API returns and what their types are. The
interface is better documentation than the API endpoint's Fiddler trace, because it's
in the codebase and the compiler enforces it. Types as living documentation is the idea
that's slowly making TypeScript mandatory on anything I start from scratch.
