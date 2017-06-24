---
layout: post
title: ".NET Core on Linux and finally meaning it"
date: 2017-06-23
author: marcetux
tags: [dotnet, linux, devops, docker]
---
We've been building .NET Core services at SolidCommerce since late 2016, containerizing
them, and running the containers on Linux EC2 instances. The conversation we never quite
finished until this month: does the Linux deployment actually work the way we think it
does, or is it working because we're careful to avoid the edge cases that don't?

The audit involved running the full integration test suite — which covers the Amazon MWS
and Walmart API clients, the RabbitMQ consumers, and the database layer — on a Linux
build agent rather than Windows. The number of failures was zero, which was the right
answer, but the audit itself was clarifying. We had a few places where file path
handling used `\\` separators hardcoded in string concatenation rather than
`Path.Combine`, which works on Windows and breaks on Linux. Found and fixed.

The .NET Core runtime on Linux has been stable for a year now and I'm genuinely using
it without caveats, which still feels worth noting given where this was in 2014. The
framework work — the libraries that assumed `System.Web`, that assumed IIS, that
assumed Windows file paths — is the thing to audit before claiming cross-platform
support. The runtime handles it; the code has to meet it halfway.
