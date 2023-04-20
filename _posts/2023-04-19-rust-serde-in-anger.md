---
layout: post
title: "Serde in anger, serialization as a design constraint"
date: 2023-04-19
author: marcetux
tags: [rust, serde, casper, serialization]
---
Casper's wire format for deploys is custom binary serialization with a precise spec
— not JSON, not Protobuf, but a bytecode layout the node expects exactly. Getting this
right in Rust means implementing Serde's `Serialize` and `Deserialize` traits, and the
exercise taught me something about Serde that I missed in the happy-path tutorials.

Serde's derive macros handle the common cases beautifully: `#[derive(Serialize,
Deserialize)]` on a struct and JSON in/out just works. But when your wire format
doesn't match what the derive macros produce — our enum variants encode as a leading
byte, not as a JSON string key — you implement the trait manually. That manual
implementation is explicit: you write the visitor, you write the serializer calls, and
the format is exactly what you say it is. The derive is a convenience, not a contract.

The design constraint this revealed: data structures should be serializable cleanly, and
when they're not it's usually a code smell about the structure itself. If you need a
custom serializer because your internal representation is awkward to explain to an
outside format, often the internal representation is the problem. The serialization
pressure is telling you something about the type. Listen to it.
