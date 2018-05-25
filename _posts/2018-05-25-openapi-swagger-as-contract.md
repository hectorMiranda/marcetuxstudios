---
layout: post
title: "OpenAPI as a contract not just documentation"
date: 2018-05-25
author: marcetux
tags: [openapi, swagger, api, architecture, documentation]
---
CTM has about a dozen client-facing REST APIs and they share a problem I've seen
before: the documentation and the implementation drift apart because nobody updates
the Confluence page when a field changes. The standard fix is to generate the
documentation from the code and make the code the source of truth. We're doing that
with Swashbuckle and OpenAPI 3.0, but the part I want to push further is treating the
spec as a contract that both sides of the API boundary are generated from.

The server generates Swagger UI and a machine-readable spec file. Nothing new there.
What we're adding is a validation step in CI that checks the generated spec against a
snapshot and fails the build if the spec changed without a version bump. The spec
is now under version control and any breaking change is a conversation, not an
accident discovered by a client when their integration breaks in production.

The longer-term goal is spec-first design: write the OpenAPI document, review the
API surface with stakeholders, then implement against it. The tooling to generate
server stubs from the spec exists and is reasonable. What it enforces is the design
conversation before implementation, which is where API design decisions belong. We've
been doing implementation-first for long enough that the spec review will be a
cultural shift, not just a tooling one.
