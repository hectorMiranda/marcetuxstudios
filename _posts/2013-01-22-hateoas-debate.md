---
layout: post
title: "The HATEOAS debate, from the trenches"
date: 2013-01-22
author: marcetux
tags: [rest, api, hypermedia, architecture]
---
Every REST discussion eventually reaches HATEOAS — hypermedia as the engine of
application state, the idea that responses should include links telling the client
what it can do next, so clients navigate your API instead of hardcoding URLs. By the
Richardson maturity model it's "real" REST. In practice, almost nobody does it, and
I've been chewing on why.

The theory is genuinely good: a client that follows links is decoupled from your URL
structure, so you can restructure endpoints without breaking it. The reality is that
client developers want a URL they can paste into a browser and a doc that lists
endpoints, and they hardcode the links anyway because it's faster.

Where I've landed: include links where they *reduce* client work — pagination
`next`/`prev`, a link to a related resource the client would otherwise construct by
hand. Skip the full hypermedia ceremony nobody consumes. Pragmatic REST: links where
they pay for themselves, not dogma for a maturity-model score.
