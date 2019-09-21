---
layout: post
title: "C# 8 nullable reference types in a real codebase"
date: 2019-09-20
author: marcetux
tags: [csharp, dotnet, nullable, architecture, quality]
---
Two weeks of nullable reference types enabled on the reconciliation service. The annotation context and warnings are in `warn` mode — they show up as warnings, not errors, while I work through the codebase. The findings are instructive.

The first category of warning is honest: methods returning reference types that can genuinely return null, not annotated as `string?` or `PaymentRecord?`. The caller was treating them as always-non-null and not checking. Approximately a third of the warnings I reviewed fell into this category — places where a null dereference could actually happen under a code path we don't exercise in tests because the input data in test environments is clean. Those get real fixes: explicit null checks, method signature changes to `?`, or restructuring to remove the null path.

The second category: the analyzer being conservative about code paths that are null-safe but the flow analysis can't prove it. The common one is a property set in a constructor through a virtual method call (don't do this, but existing code does), where the analyzer can't track the initialization. These get `!` (null-forgiving) operators with a comment explaining why the assertion is valid. The comment is the point — it documents a human judgment that the compiler can't make.

The third category, which I didn't expect: old code that defensively null-checks things that can't be null given the flow, because the author was uncertain. Those checks are now annotated as unnecessary by the analyzer, which is interesting feedback on code confidence. The nullable analysis is a kind of historical audit of where the original authors were less sure.
