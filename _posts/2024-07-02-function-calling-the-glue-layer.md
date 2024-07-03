---
layout: post
title: "Function calling is the glue layer for LLM integrations"
date: 2024-07-02
author: marcetux
tags: [llm, ai, function-calling, architecture, integration]
---
I keep explaining function calling to clients who think it's an advanced feature. It
isn't — it's the thing that makes LLM integrations practical. Without it, you're
either parsing free text output (fragile) or using JSON mode with a schema you hope
the model fills in correctly (better, but still one step removed). Function calling
is the model saying "here's the function I want to call and here are the arguments,
formatted exactly to the schema you gave me." Your code executes the function, returns
the result, and the model continues with real data in its context.

The architecture shift this enables: the model as an orchestrator, tools as
capabilities. I describe the tools I want the model to have — search the knowledge
base, look up a customer record, check an inventory level — and the model decides
which to call in which order based on the user's request. The actual tool
implementations are just Python functions. The model's job is figuring out the
sequence and the parameters; my job is writing functions the model can call safely.

The guardrail that matters: never let the model call a tool that has irreversible
side effects without a human confirmation step between the model's request and the
execution. Reads are fine to execute directly. Writes, sends, deletes — confirm.
This isn't a technical constraint; it's an architectural principle. The model's
tool-calling plans are usually correct. "Usually correct" is not the bar for
deleting a database record.
