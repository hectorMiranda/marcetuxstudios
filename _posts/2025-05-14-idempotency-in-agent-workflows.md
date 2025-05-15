---
layout: post
title: "Idempotency in agent workflows"
date: 2025-05-14
author: marcetux
tags: [agents, ai, architecture, api, distributed]
---
The idempotency problem in agent workflows is the same problem as in distributed
systems: you need every operation to be safe to retry without duplicating side effects.
The difference is that in a distributed system you're usually defending against network
retries and at-least-once delivery. In an agent workflow you're defending against the
agent deciding to retry because it didn't get confirmation, or the orchestrator restarting
after a crash, or you manually re-running a step because the previous output was wrong.

The pattern I've codified: every tool call that has a side effect takes an idempotency
key. The key is derived from the inputs — a hash of the operation type and its
parameters — so the same logical operation always produces the same key. The tool
implementation checks whether it's already run with that key, and returns the previous
result if it has. This is exactly how payment APIs handle retries, and it's the right
abstraction for agent tools too.

The implementation cost is low — a small cache keyed by operation hash, a TTL to keep
it from growing forever — and the reliability gain is significant. Agents that can
retry freely without side-effect risk are agents you can let fail and recover without
manual intervention. Make the happy path reliable by making the failure path safe.
