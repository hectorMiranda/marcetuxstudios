---
layout: post
title: "NSwag client codegen from OpenAPI specs"
date: 2019-05-15
author: marcetux
tags: [openapi, dotnet, codegen, api, tooling]
---
The promise of the contract-first API approach from January: consumer teams generate typed clients from the spec and never write HTTP plumbing by hand. We picked NSwag for the .NET consumer side, and it's been working well enough that I want to talk through what makes it actually useful versus what the unhappy path looks like.

NSwag reads an OpenAPI 3 spec and generates a C# client class with typed methods corresponding to each operation. Call `await client.InitiatePaymentAsync(request, ct)` instead of hand-assembling `HttpClient` calls, parsing responses, and mapping error codes. The generated client handles serialization, the `Authorization` header, and maps HTTP error responses to typed exceptions. It becomes a build step: the pipeline fetches the spec from the provider's artifact store, runs `nswag run`, checks in the generated client code, and the consuming project compiles against it.

The unhappy path is spec drift without versioning. The first time we updated the payment initiation spec and didn't bump the version, the consumer's pipeline regenerated a client that broke an interface the consumer had already built on. The fix is the versioned spec convention from the EDA post — you don't change `payment-initiation-v1.yaml`; you create `payment-initiation-v2.yaml` and publish both. Consumers migrate on their own schedule. Code generation gives you safety; contract discipline gives you the smooth migrations. They only work well together.
