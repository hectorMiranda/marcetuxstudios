---
layout: post
title: "Batch LLM jobs for async document processing"
date: 2024-09-25
author: marcetux
tags: [llm, ai, architecture, async, batch]
---
Not every LLM task needs an immediate response. I keep seeing systems that process
documents synchronously through a language model when the user experience doesn't
require a live answer — the processing happens in the background and the result is
displayed when the user next opens the page, or delivered by email. Treating these
as synchronous requests that block a user-facing endpoint is waste and fragility; the
right architecture is a batch queue.

The pattern: ingest job goes into a queue (SQS, Redis Queue, whatever you already
operate). A worker pool processes jobs asynchronously, calls the LLM, writes results
to the database, emits an event. The API endpoint returns a job ID immediately; the
client polls or receives a webhook when the job completes. This decouples LLM latency
from UI responsiveness, allows retries with backoff when the API rate-limits, and
lets you scale workers independently of web workers.

The pricing dimension that makes this worth writing about: OpenAI's Batch API offers
50% cost reduction for requests completed within 24 hours. For non-time-sensitive
processing — overnight summarization runs, weekly report generation, document
indexing — this is free money you're leaving on the table with synchronous
processing. The architecture is the same queue pattern; you're just targeting a
different endpoint and accepting async delivery. Know which of your LLM requests
genuinely need a live response and which ones just assumed they did.
