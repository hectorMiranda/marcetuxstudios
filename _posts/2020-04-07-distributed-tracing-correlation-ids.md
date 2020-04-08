---
layout: post
title: "Distributed tracing starting with correlation IDs"
date: 2020-04-07
author: marcetux
tags: [observability, dotnet, architecture, devops]
---
A payment flow spans four services. When it fails, the logs from each service have
timestamps in the right ballpark but nothing that links them to the same request.
Figuring out which log lines belong together means hand-matching timestamps and
inferring the path. That's archaeology, not debugging.

The minimal fix — and the right starting point before you reach for a full tracing
system — is a correlation ID: a GUID generated at the entry point and threaded through
every downstream call via a request header. Each service logs it with every line.
Finding all the log output for a single request becomes a Splunk query with one clause.
In .NET Core this fits neatly into middleware: one piece that reads or generates the
`X-Correlation-ID` header, stuffs it into the logging scope, and propagates it on
every outbound `HttpClient` call via the factory's default header.

This isn't distributed tracing in the Jaeger/Zipkin sense — there's no span graph,
no timing breakdown per service. It's a correlation primitive that costs nothing to
add and that Splunk makes immediately useful. The full tracing system is on the
roadmap; the correlation ID is what I can ship this sprint and that will make on-call
life measurably better by next week. Start with the thing that's useful now; layer
on sophistication when the simpler thing can't answer a question you're actually asking.
