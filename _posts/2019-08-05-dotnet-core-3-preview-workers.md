---
layout: post
title: ".NET Core 3.0 preview and the Worker Service model"
date: 2019-08-05
author: marcetux
tags: [dotnet, aspnetcore, workers, background-services, architecture]
---
.NET Core 3.0 is in preview and the thing I've been watching most closely is the Worker Service project template. We have a handful of background processors — Service Bus consumers, scheduled aggregators, a nightly reconciliation job — that have been running as `Console` applications with a hand-rolled main loop. The worker model formalizes what we were building by hand.

A Worker Service is a hosted process built on `IHostedService` and the generic host. You get dependency injection, configuration from Azure App Configuration, lifetime management, and a structured shutdown lifecycle — the same infrastructure as an ASP.NET Core app but without the HTTP layer. The Service Bus consumer becomes a class that implements `ExecuteAsync`, receives the `CancellationToken` from the host, and runs until cancellation is requested or it faults. The host handles startup/shutdown sequencing, signal handling on Linux (`SIGTERM` is the Kubernetes graceful shutdown signal), and logs the unhandled exception before crashing.

I've been running the 3.0 preview workers in dev and staging and they're behaving well. The hosted service pattern was already available in 2.x but the project template, the deployment model for a container with no HTTP port, and the `BackgroundService` base class for long-running workers are cleaner in 3.0. Release is expected in September. I'm ready to migrate the reconciliation job the day 3.0 goes GA because it's a good isolated target and the pattern fits it exactly.
