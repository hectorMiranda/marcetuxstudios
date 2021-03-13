---
layout: post
title: "OpenAPI as a contract not documentation"
date: 2021-03-12
author: marcetux
tags: [api, openapi, architecture, design]
---
We've had OpenAPI specs in this project for two years and they've been wrong for
most of it. Not technically wrong — Swashbuckle generates them from the code and
they're always current — but wrong about the order of operations. The spec was
downstream of the code. Someone built the endpoint, the spec described it, and
consumers discovered the shape at integration time. That's documentation, not a
contract.

The shift is writing the spec first and generating the server stubs from it. The
spec becomes the thing you hand a consumer team on Monday so they can start
building client code while we build the service. It forces the conversation about
field names, response shapes, and error codes to happen before anyone has written
code they'll defend in review. Arguments about API design are cheap when both sides
are editing YAML; they're expensive when one side has three weeks of consumer code
that assumes a field named differently.

We prototyped this with a new reporting endpoint and the feedback loop was
immediately better. The consumer team flagged two response fields that were
ambiguous before a single line of server code existed. The final implementation
matched the contract because the contract was the design. It's not a huge process
change — spec-first is one decision — but it moves the disagreement from integration
to design where it belongs.
