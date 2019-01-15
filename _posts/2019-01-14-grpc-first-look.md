---
layout: post
title: "gRPC a first look from a REST shop"
date: 2019-01-14
author: marcetux
tags: [grpc, protobuf, api, architecture, dotnet]
---
We have a REST-everywhere culture at the bank — every internal service speaks JSON over HTTP, which is fine and interoperable and occasionally very slow when you're streaming high-frequency data between internal systems. I pulled gRPC into a proof-of-concept this weekend and now I have opinions.

The protocol buffer IDL is the thing that clicked. You define your service in a `.proto` file, run the code generator, and get strongly-typed clients and server stubs in whatever language you target — which is what I wanted from OpenAPI but with the actual wire format enforced, not just the shape. The binary encoding is meaningfully smaller than JSON for the numeric data we're moving, and HTTP/2 framing means you get multiplexed streams over a single connection. For an internal service-to-service call that happens ten thousand times a minute, that matters.

The honest downside is observability: JSON is human-readable in a Wireshark capture or a Splunk log; protobuf binary is not. Tooling around gRPC is improving fast but it's still rougher than the REST ecosystem. For client-facing APIs, REST stays. For internal high-frequency data flows where both sides are under my team's control — gRPC is the right answer, and the `.proto` file is an even stricter contract than an OpenAPI spec.
