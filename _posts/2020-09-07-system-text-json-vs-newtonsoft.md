---
layout: post
title: "System.Text.Json in .NET 5 and closing the Newtonsoft gap"
date: 2020-09-07
author: marcetux
tags: [dotnet, csharp, json, performance]
---
The bank has six production services, and all six use Newtonsoft.Json. That's not
a problem — Newtonsoft is excellent and going nowhere — but `System.Text.Json` has
been Microsoft's answer since .NET Core 3.0, and every preview cycle has closed a
feature gap. With .NET 5 RC1 available this month, I wanted to verify that the gaps
relevant to us are actually closed.

The two that blocked migration before: support for reference cycles (circular object
graphs) and more flexible constructor-based deserialization. Both are in .NET 5.
`ReferenceHandler.Preserve` handles cycles the way Newtonsoft's `PreserveReferencesHandling`
does. The new constructor-binding behavior means immutable record types — which C# 9
records are — deserialize correctly without custom converters. The things I was
working around are no longer workarounds.

The performance argument is real but not the one I lead with internally. `System.Text.Json`
is faster and allocates less than Newtonsoft on the benchmarks that match our access
patterns. For services under load that's meaningful. But "replace a working dependency"
is a hard sell on performance alone. "Replace it because it's now the BCL default,
it supports our new C# 9 patterns, and it removes a third-party dependency" is a
different conversation. I'll pilot it on the next new service and migrate the existing
ones opportunistically. Not all at once; one at a time, when the reason to touch
the code exists anyway.
