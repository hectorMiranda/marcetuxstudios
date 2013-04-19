---
layout: post
title: "Dependency injection in Web API without a framework first"
date: 2013-04-18
author: marcetux
tags: [dotnet, webapi, architecture, testing, di]
---
When the new developer joined the team, I tried to explain why we use a DI container
and watched his eyes glaze at the XML configuration. The container is Ninject, and it
is not a simple thing to explain to someone who hasn't needed it. So this sprint I
wrote the same controller twice — once with `new` calls inside, once with constructor
injection and no container — just to show that the principle exists before the
framework does.

The insight is that a controller with `new DataRepository()` in its constructor is
impossible to unit-test without hitting the database, because the dependency is
hardcoded. Change the constructor to accept `IDataRepository` instead and suddenly a
test can pass a mock. The controller tests whether *it* does the right thing; the
repository tests whether *it* does the right thing; they don't need to test together.

Once that clicks, the DI container is just a factory that builds the dependency tree
for you at runtime — you describe the mapping (when someone asks for `IDataRepository`,
give them `SqlDataRepository`), and the container reads constructor parameters and
wires everything up. The container is a convenience; the *principle* is that you name
what you depend on instead of creating it. Teaching the principle first made the Ninject
config make sense in about five minutes instead of forty.
