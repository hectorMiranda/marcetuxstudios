---
layout: post
title: "Evaluating Sidekiq alongside the SQS worker setup"
date: 2014-12-13
author: marcetux
tags: [ruby, rails, sidekiq, redis, sqs, background-jobs]
---
The team has a standing discussion about whether the SQS workers should be replaced
with Sidekiq, which uses Redis for queue storage and is more idiomatic in the Rails
world. I spent this week doing a real evaluation rather than arguing from priors, and
the answer is "it depends on what you care about."

Sidekiq is faster and simpler to develop with. The worker is a Ruby class with a
`perform` class method; `JobClass.perform_async(arg1, arg2)` enqueues it. The web UI
for monitoring the queue is included. Running Sidekiq in development is `bundle exec
sidekiq`; no AWS credentials required. For a Rails app where the workers and the app
share the same codebase and Redis is already a dependency (which it is at Spark for
sessions), Sidekiq is the path of least resistance.

SQS has durability and visibility semantics that Sidekiq doesn't match by default. A
message in SQS survives the Redis server dying. A visibility timeout means work isn't
lost if a worker crashes mid-job. For the photo processing and email delivery jobs —
where at-least-once and no-data-loss matter — SQS's durability guarantees are worth
the operational overhead. My recommendation: Sidekiq for jobs where a lost job is a
nuisance, SQS for jobs where a lost job is an incident.
