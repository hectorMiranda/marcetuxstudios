---
layout: post
title: "IHttpClientFactory and the socket exhaustion problem"
date: 2020-02-18
author: marcetux
tags: [dotnet, csharp, architecture, performance]
---
A service in the bank's integration layer was throwing intermittent connection errors
that disappeared on restart and came back after a day or two of traffic. The pattern
is familiar once you've seen it: socket exhaustion from `new HttpClient()` inside a
service method. Every call creates a new client, opens a socket, and although the
client disposes and closes the connection, the socket lingers in TIME_WAIT for two
minutes. Enough calls per minute and you run out of ephemeral ports.

`IHttpClientFactory` is the fix that shipped with .NET Core 2.1 and that I should
have been using since 2018. You register named clients in `Startup`, each with its
base address, default headers, and optional Polly retry policies. The framework
manages the underlying `HttpMessageHandler` pool, reusing connections where possible
and rotating handlers before DNS-change blindness sets in. The consuming service just
calls `_factory.CreateClient("payment-gateway")` and gets a client already configured
for that upstream.

The migration was surgical — find every `new HttpClient`, replace with an injected
factory, register the named client in DI, done. Intermittent errors stopped the
same day. The lesson I keep relearning: the framework has already solved the problem
you're solving by hand. It's usually better at it.
