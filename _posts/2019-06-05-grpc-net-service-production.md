---
layout: post
title: "Moving a gRPC service to production"
date: 2019-06-05
author: marcetux
tags: [grpc, dotnet, aspnetcore, architecture, production]
---
The gRPC proof-of-concept from January turned into a production service last month. A real-time account balance aggregator — reads balances from four upstream sources, merges them, and streams the result to a downstream risk scoring service ten times per second. JSON over HTTP was doing fine until volume tripled; gRPC streams over HTTP/2 cut the overhead enough to matter and the protobuf contracts made the integration story cleaner than the REST equivalent would have been.

The production hurdles were mostly infrastructure. gRPC requires HTTP/2 end-to-end, and Azure Application Gateway at the time had awkward HTTP/2 termination behavior for backend connections. We routed the service behind the Azure Load Balancer directly, outside the App Gateway path, and enforced access via our mTLS policy instead. The second hurdle: generating client libraries for the consuming service in a CI pipeline. We run `protoc` in the pipeline and publish the generated NuGet package to our internal Azure Artifacts feed. The consumer pins a specific version of that package, just like any other dependency.

What I'd do differently: the proto schema lives in its own repository with its own versioning. We started with the proto in the producer's repo and the consumer had to read a foreign repository's git history to understand why a field changed. Moving the proto to a contracts repository with a changelog and semantic versioning rules made the contract the first-class thing, which is the same discipline we applied to OpenAPI. The wire format is the thing both sides agree on; it deserves its own home.
