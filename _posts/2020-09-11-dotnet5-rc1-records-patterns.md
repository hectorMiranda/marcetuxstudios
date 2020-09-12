---
layout: post
title: ".NET 5 RC1 - records and pattern matching in practice"
date: 2020-09-11
author: marcetux
tags: [dotnet, csharp, language, records]
---
RC1 dropped this week and the go/no-go on records is go. I've been using them in
preview builds on internal tools, but RC means the feature is locked and I can
reason about it with more confidence. The use case that most benefits isn't the
obvious one — not DTOs, surprisingly — but domain events.

A domain event is an immutable value that describes something that happened. Before
records: a class with a constructor, private setters, an override for Equals and
GetHashCode, maybe an IEquatable implementation. Six to twelve lines, all ceremony.
After records: `record PaymentReceived(string PaymentId, decimal Amount, DateTimeOffset At);`
One line. Structural equality comes for free — two `PaymentReceived` records with the
same field values are equal by value, which is what you want for deduplication and
testing. The `with` expression lets you clone and modify immutably: `existing with { Amount = corrected }`.

The enhanced pattern matching in switch expressions is where the complexity payoff
appears. Dispatching on an event type and simultaneously destructuring its fields
into local variables is now a single expression with no casts, no null checks, no
intermediary variables. Code that was twelve lines of if/else with type checks is now
a switch expression that reads like a spec. It doesn't make code correct; it makes
the wrong structure harder to write by accident.
