---
layout: post
title: "C# 9 records in production code three months in"
date: 2020-11-18
author: marcetux
tags: [csharp, dotnet, language, architecture]
---
Since I started using C# 9 records in September preview builds, they've landed in
three production services. The usage patterns that have emerged are clearer than
the preview experiments suggested, and the places where records are wrong are as
important as the places where they're right.

Records are correct for things that are value-like by nature: domain events, query
results, DTOs that cross a serialization boundary, configuration-as-value. They're
wrong for entities — the domain objects whose identity persists across mutations.
An `Account` record with structural equality means two accounts with the same balance
and owner are "equal," which is nonsensical. A `PaymentReceivedEvent` with structural
equality means two events with the same fields are "equal," which is correct for
deduplication. The type tells you whether identity is structural or referential.

The `with` expression has been the most useful feature in practice. In our event-
sourcing code, we reconstruct state by applying events to an initial value: each
event handler returns `state with { Balance = state.Balance + evt.Amount }`. The
immutability is enforced by the type; there's no accidental mutation of a state
object that's shared elsewhere. Three months in, the category of bug where a domain
object was mutated somewhere unexpected has essentially vanished from our reviews.
That's the kind of language feature that earns its keep quietly.
