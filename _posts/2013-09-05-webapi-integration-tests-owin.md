---
layout: post
title: "Web API integration tests with OWIN self-host"
date: 2013-09-05
author: marcetux
tags: [dotnet, webapi, testing, owin, integration-tests]
---
Unit tests on the Angular services plus unit tests on the C# service layer leaves an
untested gap: the Web API controllers, routing, model binding, and filter chain all
behave correctly together only in a running app. The standard answer for that gap is
a separate integration test project that fires up an OWIN self-hosted Web API, makes
real HTTP calls, and asserts real HTTP responses.

The OWIN self-host approach runs the full `Startup.cs` pipeline inside the test process
using `Microsoft.Owin.Testing.TestServer`. The test gets an `HttpClient` pointed at the
test server, calls an endpoint, gets back the real response including status code and
body. The middleware runs, the routing matches (or doesn't), the model binding happens,
the filters fire — everything that wouldn't be tested by calling a controller method
directly.

The tests run in memory without a network socket, which keeps them fast enough to run
on every build. The test data challenge is separate — I swap the DI registrations for
in-memory fakes of the database layer, so tests don't need a database. That swap is why
the DI setup from April pays off: the test project just calls `kernel.Rebind<IDataRepository>
().To<InMemoryDataRepository>()` and the rest of the setup stays the same. The integration
tests found two cases where the routing matched a URL I didn't expect and one where the
model binder silently dropped a required field on `Content-Type: text/json`. Worth it.
