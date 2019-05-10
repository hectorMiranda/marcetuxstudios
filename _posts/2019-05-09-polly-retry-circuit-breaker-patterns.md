---
layout: post
title: "Polly retry and circuit breaker in practice"
date: 2019-05-09
author: marcetux
tags: [dotnet, resilience, polly, architecture, integration]
---
The payment service calls three external systems. Each of those systems has its own maintenance windows, its own rate limits, and its own capacity for being briefly unavailable. Before resilience policies, a third-party blip meant our service either surface-errored immediately or hung indefinitely waiting for a connection timeout. Neither is acceptable when the banking portal is in front of a customer trying to move money.

Polly handles this and it's part of the `HttpClientFactory` setup now, which means every `HttpClient` we create can have a retry and circuit breaker policy without any ceremony in the calling code. The retry policy is exponential backoff with jitter — three retries at 1s, 4s, 16s ± some random noise, so that a mass of clients recovering from an outage don't hammer the recovering service in synchronized waves. The circuit breaker opens after five consecutive failures and stays open for thirty seconds before trying again. The calling code doesn't know any of this is happening; it just awaits an HTTP call.

The part I had to get right was what to retry. Transient failures — 503, 429, connection refused, timeout — are fair game. A 400 Bad Request is not a transient error; retrying it just sends the same bad data again faster. The policy has an explicit allow-list of retriable status codes. And a 401 or 403 is never transient — if the token is rejected, retrying while the token is invalid burns retries and the circuit breaker threshold. Auth errors go straight to the caller as failures. Know what you're retrying and why; don't retry everything and call it resilient.
