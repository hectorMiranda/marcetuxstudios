---
layout: post
title: "Structured output is how you actually integrate LLMs"
date: 2024-01-20
author: marcetux
tags: [llm, ai, architecture, json, integration]
---
The most common failure mode I see in early LLM integrations is treating the model
like a human: you ask it a question in prose and then try to parse prose back out.
That works in a demo and falls apart in a pipeline. The model decides to add a
preamble, a caveat, or an apology for the weather, and your downstream code breaks
because it expected JSON.

The fix is to force the output shape. OpenAI's JSON mode locks the model to valid
JSON. Give it a schema via the system prompt — field names, types, required vs.
optional — and the model fills in values instead of composing sentences. I've been
doing this for a document-extraction workflow: the input is a messy vendor invoice,
the output is a Python dataclass, and the intermediate representation is JSON the
model is constrained to produce. The error rate dropped from "requires cleanup every
few runs" to "ran 200 invoices and found two legitimate edge cases I could fix in
the schema."

The discipline is designing the schema before you write the prompt. The schema is
the contract; the prompt is just the instruction to fill it. When outputs are wrong,
look at the schema first — ambiguous field names produce ambiguous values. Precise
schemas produce precise answers. Once the shape is right, the model is just a
complicated form-filling engine, and that's exactly what you want it to be.
