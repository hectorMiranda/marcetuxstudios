---
layout: post
title: "C# 9 records for domain value objects"
date: 2021-03-18
author: marcetux
tags: [csharp, dotnet, domain-modeling, design]
---
I've been using C# 9 records for two months now with a specific use case in mind:
value objects in the domain layer. Money amounts, account identifiers, transaction
codes — types that have no identity beyond their value and where equality means
"same value," not "same object reference." Before records, making a proper value
object meant implementing `Equals`, `GetHashCode`, and `==` overloads, which is
about thirty lines of boilerplate for a type that represents a concept in one line.

Records collapse all of that. Declare `record Money(decimal Amount, string Currency)`
and you get structural equality, a copy constructor via `with`, and a readable
`ToString` for logging. The compiler generates what you'd have written anyway,
minus the errors you'd have made writing it at 11 p.m. The `with` expression is
the one I didn't expect to appreciate as much as I do — creating a modified copy
without mutation fits the immutable-domain-model style naturally.

The one limit worth knowing: records are reference types by default, which means
they're heap-allocated. For value objects created in large loops or hot paths,
`record struct` from C# 10 is the answer — it gives you the same generated members
as a stack-allocated value type. I'm not there yet (we're on C# 9 / .NET 5), but
it's clearly where the language is going, and the migration path will be mechanical
when we get to it.
