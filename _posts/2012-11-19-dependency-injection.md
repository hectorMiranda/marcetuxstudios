---
layout: post
title: "Dependency injection without the religion"
date: 2012-11-19
author: marcetux
tags: [csharp, dotnet, architecture, testing]
---
The codebase reached the size where `new`-ing dependencies inside classes started
hurting — every class hard-wired to concrete types, untestable without standing up
the whole world. Dependency injection is the unwind, and it's simpler than the
framework zealotry makes it sound.

The core idea has nothing to do with a container: **ask for your dependencies in the
constructor instead of creating them.** A class that takes an `IReportStore` can be
handed the real SQL one in production and a fake in a test. That's it. That's the
testability win, and you get it with plain constructors and zero libraries.

The container (Ninject, Unity, whatever) only earns its place once you're tired of
wiring the object graph by hand at startup. Useful, not sacred. I've seen teams
spend more time configuring the container than they ever saved. Inject the
dependencies; adopt the container when the manual wiring actually hurts.
