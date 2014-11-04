---
layout: post
title: ".NET going open source and why it matters"
date: 2014-11-03
author: marcetux
tags: [dotnet, microsoft, open-source, csharp, history]
---
Microsoft announced this week that .NET Core — the new cross-platform runtime — is
going open source under the MIT license. The full stack: the runtime, the core class
libraries, and the compiler platform Roslyn. If you'd asked me two years ago whether
this would happen I'd have said probably not in my career. I was wrong.

The practical implication isn't immediate for the code I'm writing at Spark, which is
Ruby and doesn't involve .NET. But for the two years I spent at Edgecast in the
Microsoft ecosystem, this changes what that ecosystem becomes. .NET on Linux means the
same application server on the same OS as your Ruby and Python apps. Containers for
.NET stop requiring Windows Server, which changes both the operational model and the
cost structure. The ASP.NET vNext redesign — which becomes ASP.NET Core — is a modern
web framework that isn't welded to IIS or `System.Web`.

I spent two years on the OWIN path precisely because it was pointing at this — getting
the web application layer off the Windows-specific plumbing. The announcement validates
that direction. For someone coming back to .NET after a period away, the ecosystem
you return to in 2015 or 2016 will be different in the ways that made it frustrating.
Worth watching even from Ruby-land.
