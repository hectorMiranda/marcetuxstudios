---
layout: post
title: "Contract-first APIs with OpenAPI and what it changes"
date: 2020-01-27
author: marcetux
tags: [api, openapi, dotnet, architecture]
---
Every API project at the bank still starts the same way: somebody writes controllers
until something works, then exports a Swagger doc from the running app and calls it a
spec. It looks contract-first; it's code-first with documentation lag. The client
team gets surprised when the shape changes, and the spec is always one commit behind
reality.

The actual contract-first flow starts with the OpenAPI yaml. Design the paths, the
schemas, the error responses — before writing a line of C#. Get sign-off on the
contract from the consumers. Then use the spec to generate the server stubs and the
client SDKs. When the generated stub diverges from what you coded, the spec wins.
The code is an implementation of an agreed contract, not the other way around.

What changes practically: the consumer can build against the mock server while the
implementation is in progress, because the contract is pinned before implementation
starts. The integration meeting goes from "does this work?" to "does this match the
spec?" — two different questions with very different answers. The yaml file becomes
the artifact that matters; the controllers are a delivery mechanism. It's the kind of
inversion that feels annoying until it saves you a broken-integration deploy.
