---
layout: post
title: "Mocking HTTP calls in C# unit tests"
date: 2014-05-19
author: marcetux
tags: [dotnet, testing, csharp, mocking, unittesting]
---
The services that wrap external HTTP dependencies have been difficult to test because
they use `HttpClient` directly, and `HttpClient` isn't interface-based in a way that
supports easy substitution. For a while my integration tests hit real endpoints, which
means they require network access, fail on blocked ports in CI, and are slow. There's
a better way.

`HttpClient` accepts an `HttpMessageHandler` in its constructor. The handler is the
piece that actually sends the request. Injecting a fake handler instead of the default
`HttpClientHandler` lets you intercept the call, return a pre-crafted response, and
assert on what was sent — without touching the network. The fake handler is about twenty
lines: override `SendAsync`, check the request URL, and return `new HttpResponseMessage`
with whatever status and content the test scenario needs.

The wrinkle is that `HttpClient` is meant to be long-lived and shared — instantiating a
new one per test is a socket exhaustion anti-pattern. The solution is injecting the
factory pattern: a service factory or a static factory method creates one client per
host and reuses it. Tests inject a mock factory that returns clients backed by the
fake handler. It's a bit of plumbing, but once the wrapper exists every HTTP-dependent
service test gets it for free.
