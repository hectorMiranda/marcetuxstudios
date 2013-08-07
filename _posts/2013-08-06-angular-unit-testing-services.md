---
layout: post
title: "Unit testing Angular services with Jasmine and ngMock"
date: 2013-08-06
author: marcetux
tags: [javascript, angular, testing, jasmine, frontend]
---
The Angular services I extracted back in February have been tested manually for six
months, which is not the same as tested. Added Jasmine specs this week with the help of
`angular-mocks`, and the combination is the right environment for service-level tests.

`ngMock` provides `$httpBackend`, a mock that intercepts `$http` calls and lets tests
specify what the server would have returned — or assert that certain requests were made
— without actually hitting the network. A service test can verify that a `GET` to
`/api/v1/customers/5` is made when the service's `get(5)` method is called, and that
the resolved value matches the mocked response, all within a Jasmine `it` block that
runs in milliseconds. The test describes the contract between the service and the API.

The mechanics: wrap each test in `angular.mock.module('dash')` to set up the injector;
use `inject($httpBackend, Customer)` to get the real service (not a mock) and the fake
backend. Call the service method, call `$httpBackend.flush()` to synchronously resolve
the pending request, then assert. The `$httpBackend.verifyNoOutstandingExpectation()`
in `afterEach` ensures I didn't set up an expectation and forget to trigger it. It's
more setup than I'd like, but the alternative is six-month-old untested service code,
which is worse.
