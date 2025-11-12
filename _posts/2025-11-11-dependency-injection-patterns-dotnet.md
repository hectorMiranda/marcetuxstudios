---
layout: post
title: "Dependency injection patterns I return to in .NET"
date: 2025-11-11
author: marcetux
tags: [dotnet, csharp, architecture, backend, di]
---
A client codebase had a DI registration file that was three hundred lines long and
growing, with a mix of interface registrations, factory methods, and options
configurations in no particular order. It worked, and it was impossible to reason about.
Cleaning it up reminded me of the patterns I'd internalized over years that I don't
always articulate.

The one that matters most: register at the boundary, not in the middle. Services
registered with `AddScoped` or `AddSingleton` should be the things your handlers and
controllers ask for, not the internal implementation details of those services. If the
internal details need to be wired together, that wiring lives inside the service's
registration extension method, not in the root container. The root container describes
the shape of your application; the internal wiring is a private concern of the module
it belongs to.

The second pattern: name your extension methods after the feature they enable, not
after the class they register. `services.AddReportingService()` tells me what the
application can do. `services.AddSingleton<IReportingService, ReportingServiceImpl>()`
tells me about the implementation. Both register the same thing; one explains the
capability, the other explains the plumbing. The root registration file should read like
a configuration of capabilities, not a manifest of classes.
