---
layout: post
title: "Identity tokens at the API gateway boundary"
date: 2021-01-11
author: marcetux
tags: [security, jwt, api-gateway, architecture, azure]
---
An audit finding came back about bearer tokens surfacing in backend service logs.
The fix sounds simple until you pull on it: the gateway was forwarding the raw
Authorization header downstream as-is, so every microservice in the cluster was
logging a full JWT it had no business seeing. We treat logs like internal-only
scratchpads, but they're not — they flow into Splunk, get archived, and get
exported for compliance review.

The right boundary is to exchange credentials at the edge. The gateway
authenticates the incoming token once, extracts the claims it needs, and then
either strips the header or replaces it with an internal assertion that downstream
services can trust without holding the user's actual bearer token. Azure API
Management has policy expressions to do exactly this; we were just letting
everything pass through because nobody had wired up the policy yet.

Getting the policy in place was a half-day of APIM XML — not the glamorous end of
architecture work, but this is where the real security properties live. Not in the
design doc, not in the threat model spreadsheet: in the actual policy that runs
at the actual boundary. Documentation of what you intended to do is not the same
as the configuration that does it.
