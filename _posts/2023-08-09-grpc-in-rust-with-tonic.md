---
layout: post
title: "gRPC in Rust with Tonic"
date: 2023-08-09
author: marcetux
tags: [rust, grpc, casper, networking]
---
The Casper node exposes a gRPC streaming endpoint for events — new block headers,
deploy results, deploys entering and leaving the pending queue. Consuming it cleanly
from Rust took a few iterations, and the library that made it work is Tonic, the
de-facto async gRPC implementation for Rust.

The workflow: write your `.proto` file defining the service and message types, use
`prost-build` in a build script to generate the Rust types, and then use Tonic's
client API to connect and consume the stream. Tonic integrates with Tokio natively
so streaming responses come back as async `Stream` implementations — you `await` each
item as it arrives, and the backpressure is handled by the transport. The build script
pattern is a little unusual coming from languages where codegen is a standalone tool
step, but it means the generated types stay in sync with the `.proto` as long as you
don't break the build.

The rough edge: generated types are verbose and don't always map naturally to the
domain model you want in application code. I wrote a thin translation layer between
the protobuf-generated types and the cleaner structs I use in business logic. That
boundary — proto types at the wire, domain types inside — is the same discipline I'd
apply with any external format, and it meant refactoring the domain types later didn't
require touching the gRPC deserialization code.
