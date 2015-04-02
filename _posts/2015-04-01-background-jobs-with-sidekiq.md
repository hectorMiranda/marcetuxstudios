---
layout: post
title: "Background jobs with Sidekiq in Rails"
date: 2015-04-01
author: marcetux
tags: [ruby, rails, sidekiq, redis, backend]
---
Email sending was blocking the Rails request cycle — welcome email, match notification,
daily digest — and the P99 response time on any controller action that triggered an
email was embarrassing. Sidekiq moved that work out of the request and into Redis-backed
background workers, and the controller actions got their time back.

Sidekiq's model is a worker class with a `perform` method and a Redis queue it reads
from. The controller enqueues: `NotificationWorker.perform_async(member_id, :match)`.
The Sidekiq process, running alongside Rails, pulls the job from Redis and calls
`NotificationWorker.new.perform(member_id, :match)`. The request returns immediately;
the email goes out seconds later. Retries are built in — failed jobs get retried with
exponential backoff, visible in the Sidekiq web UI alongside queue depth and failure
counts.

The discipline that I didn't know I needed: make your arguments simple. Pass a member
ID, not a Member object — the worker re-fetches from the database when it runs, which
means you get the current state, not a snapshot from when the job was enqueued. Passing
serialized ActiveRecord objects leads to stale data and bloated Redis payloads. Push an
ID, pull the record fresh. The job queue is not a place to carry domain objects; it's a
place to carry references to them.
