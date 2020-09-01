---
layout: post
title: "August notes"
date: 2020-08-31
author: marcetux
tags: [meta, retrospective]
---
August was deep-stack month. gRPC is running in production on the pricing service and
the contract discipline alone has been worth the migration. Azure App Configuration
means feature flags are a portal operation rather than a deploy, which removed a
category of "coordination required" from the process overhead. ADRs are in the repo
and two new team members have already cited them in PR conversations.

The structured logging push is slower — cultural change always is — but the services
I own are structured throughout now. When I compare the Splunk queries I write against
my services versus the ones I write against the legacy freeform logs, the time
difference is embarrassing. It's the kind of investment that looks optional until
you're debugging something at midnight.

September: .NET 5 RC1 is likely next month. I want to look at the new LINQ and the
`System.Text.Json` improvements — the bank has a few places that are still using
Newtonsoft and I want to understand whether the built-in serializer is finally
good enough to close that gap. Also, the Pi cluster needs a metrics stack — I've
been watching it with `kubectl top` like an amateur.
