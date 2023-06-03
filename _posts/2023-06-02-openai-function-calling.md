---
layout: post
title: "OpenAI function calling and structured output"
date: 2023-06-02
author: marcetux
tags: [llm, openai, ai, api]
---
OpenAI released function calling in the API this week and it's the feature I've been
waiting for. The problem with getting structured output from a language model was
always that you were asking the model to produce valid JSON by putting "respond in
JSON" in the prompt and hoping — which works until it adds a trailing comma or wraps
the JSON in markdown fences. Function calling is different: you declare functions as
JSON Schema, the model decides whether to call one, and if it does, it returns the
arguments as structured JSON guaranteed to match your schema.

The mechanism: you pass a `functions` array with each function's name, description,
and parameter schema. The model reads the description as context for when to call it
and the schema as the contract for the arguments. Instead of free text, the model can
return a `function_call` object with the function name and parsed arguments. You
execute the function with those arguments and feed the result back to the model in
the next turn.

For the RAG pipeline this changes the interaction pattern: instead of parsing
"the answer is X with source Y" from prose, I can define a `cite_and_answer` function
that requires the model to name the source chunk IDs along with the answer text. The
model fills in the schema fields or doesn't call the function at all. No more regex
on model output for the common case. This is the feature that makes multi-step tool
use reliable enough to build on.
