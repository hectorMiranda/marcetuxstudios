---
layout: post
title: "Azure APIM policy patterns that pay off"
date: 2021-03-03
author: marcetux
tags: [azure, api-gateway, apim, architecture]
---
Six months into using Azure API Management as the front door for a dozen internal
services, I've settled on a short list of policies that carry their weight versus
the ones that sound good in documentation but create more problems than they solve.
The gateway is only useful if the team understands what it's doing; mysterious policy
XML is just mystery code in a different location.

The policies I keep: JWT validation at the gateway before the request reaches any
service, rate limiting per subscription key on anything public-ish, and a request-
ID header injection so traces from the gateway through every downstream service
carry the same correlation ID into Splunk. Those three together mean authentication
is tested once in one place, runaway clients get throttled before they can hurt
shared services, and a five-minute investigation replaces a two-hour one when
something goes wrong. That's a good return on XML.

The policy I removed: response caching. In theory it reduces backend load;
in practice it produced stale data bugs that were nearly impossible to diagnose
because the freshness rules in the policy didn't match the freshness rules the
backend itself enforced. Two truth sources about when data was valid is a coherence
problem waiting to happen. Caching that close to the request path needs to live
behind the service, not in front of it, where the service's own invalidation logic
can govern it.
