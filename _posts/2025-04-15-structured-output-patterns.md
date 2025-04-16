---
layout: post
title: "Structured output from LLMs is table stakes now"
date: 2025-04-15
author: marcetux
tags: [llm, ai, apis, json, architecture]
---
Six months ago getting reliable JSON out of an LLM required careful prompt engineering
and retry logic for the inevitable malformed responses. The model providers have shipped
structured output modes — constrained decoding that guarantees the response matches a
schema — and the difference in reliability is large enough that there's no reason to
use the old approach for any new production feature.

The workflow I use: define the schema as a Pydantic model (or a JSON Schema if the
client environment is not Python), pass it to the API in the structured output
parameter, and treat the response as a typed object on the other side. No parsing, no
retry-on-parse-failure, no defensive null checks for fields that "should" always be
present. The schema is the contract; the API enforces it. This is the same discipline
as typed function signatures — you write the contract once and stop defending against
the thing the contract already rules out.

The practical caveat is that constrained decoding works best when the schema is flat or
shallowly nested. Very deeply recursive schemas can hit performance issues or length
limits on the constrained token budget. For those cases I still fall back to a simpler
schema plus validation, but that's a minority of real use cases. The default is now:
define the type, let the API guarantee it.
