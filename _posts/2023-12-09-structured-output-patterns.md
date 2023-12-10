---
layout: post
title: "Structured output patterns that actually work"
date: 2023-12-09
author: marcetux
tags: [llm, ai, architecture, api]
---
The consulting work this month involves a pipeline that needs structured JSON from a
language model as part of a larger data processing workflow. The "just ask for JSON in
the prompt" approach worked in development and failed three times in production when
the model added explanation text above the JSON, or included a trailing comma, or
wrapped the JSON in a markdown fence. Three different failure modes in the first week.

The reliable approaches, in order of preference: OpenAI's function calling (the model
returns structured arguments that match a JSON Schema, guaranteed), Pydantic + output
parsing (define the schema in Python, use LangChain's Pydantic output parser to
validate and retry on failure), and outlines-based constrained decoding (steer the
token sampling to only produce tokens that keep the output valid JSON at each step).

The third approach — constrained decoding — is the most general and works on local
models, but requires a library like `outlines` that can hook into the token sampling
process. It's not available through the OpenAI API. For hosted models, function calling
is the reliable answer. For local models, constrained decoding is the reliable answer.
For everything in between, add a retry loop that re-sends the prompt with "your
previous response was not valid JSON, please try again" and parses on each retry. It's
not elegant, but it works and the retry rate in practice is low.
