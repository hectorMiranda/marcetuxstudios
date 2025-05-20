---
layout: post
title: "Routing between local and hosted models by task"
date: 2025-05-19
author: marcetux
tags: [llm, local-models, ai, architecture, consulting]
---
The pattern I've settled on for cost-sensitive client work: not one model for
everything, but a routing layer that sends each task to the appropriate model based on
task type and required capability. Code generation and multi-step reasoning go to a
hosted frontier model. Classification, extraction, and summarization of short texts go
to a local model. The cost ratio between the two categories is large enough to matter
on any engagement with significant volume.

The routing logic doesn't need to be sophisticated. A simple rule table keyed by task
type — classification tasks go local, generation tasks go hosted — handles 80% of the
calls correctly. The remaining 20% are the calls where the local model's output quality
isn't sufficient and you need to escalate. Detecting that requires an output quality
check, which can itself be a local classifier: "did this extraction include all required
fields?" is a classification task, so you can eval it locally for free.

The harder question is latency. Local models on a GPU box in the studio are fast for me;
local models deployed at a client with no dedicated GPU hardware are slow. The routing
decision has to account for the deployment context, not just the task type. In a
constrained environment the routing tilts toward fewer local calls, which tilts the cost
back up. No free lunch, but at least the options are explicit.
