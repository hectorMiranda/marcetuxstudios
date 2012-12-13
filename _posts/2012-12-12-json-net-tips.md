---
layout: post
title: "A few Json.NET habits"
date: 2012-12-12
author: marcetux
tags: [csharp, json, jsonnet, api]
---
Json.NET is so good it's easy to use thoughtlessly, and thoughtless serialization
leaks your internals onto the wire. A few habits I've adopted after cleaning up an
API that returned its EF entities raw.

**Serialize DTOs, not domain objects.** The moment you return your data model
directly, every property rename is a breaking API change and every lazy-loaded
navigation property is a surprise query (or a circular-reference explosion).
**Control the contract:** camelCase the output for the JavaScript clients, omit
nulls to keep payloads lean, and use `[JsonProperty]` to decouple wire names from
C# names so I can rename a field without breaking a customer.

And `ReferenceLoopHandling` — set it deliberately. The default throwing on circular
references has saved me from shipping an infinite document more than once, which is
the right default. Decide your contract; don't let the serializer's reflection
decide it for you.
