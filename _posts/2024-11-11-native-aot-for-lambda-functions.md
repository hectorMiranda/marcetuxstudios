---
layout: post
title: "Native AOT for Lambda functions, the case for cold start"
date: 2024-11-11
author: marcetux
tags: [dotnet, lambda, aws, native-aot, serverless]
---
Cold start latency on .NET Lambda functions has been a recurring complaint for years.
The JIT compile-on-first-run story is bad for serverless: first invocations after
scaling take seconds instead of milliseconds. The workarounds — pre-warming, Lambda
SnapStart — add complexity and cost. Native AOT is the structural answer: compile
everything ahead of time, deploy a native binary, start in under 100ms.

The catch in earlier .NET versions was compatibility. Native AOT requires that
everything used at runtime is known at compile time — no reflection over types that
aren't in the compile graph, no dynamic code generation. AWS Lambda's managed runtime
and several popular NuGet packages used reflection patterns that broke AOT. .NET 9
closed most of those gaps on the framework side, and the Lambda managed runtime for
.NET 9 ships with AOT support as a first-class deployment option.

I tested it on a consulting client's API Gateway Lambda: cold start went from 2.4
seconds to 110ms. The warm invocation latency didn't change meaningfully. Binary
size went up — AOT binaries carry more than a trimmed JIT assembly — but Lambda
pricing on binary size is negligible. The compilation time in CI extended by about
ninety seconds because AOT is a more intensive compilation pass. That's the trade:
CI time for cold start time. For a Lambda that handles production traffic with
scaling events, that's obviously the right trade. For a low-traffic background job
that rarely cold-starts, it's less clear.
