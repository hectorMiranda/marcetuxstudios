---
layout: post
title: "Dynatrace APM in a regulated environment"
date: 2019-04-15
author: marcetux
tags: [observability, dynatrace, apm, banking, architecture]
---
The bank approved Dynatrace as the APM platform, and the rollout has been educational — not because the tool is hard to use but because getting security approval for an agent that instruments every process and sends telemetry outside the network perimeter took nearly four months. I don't say that as a complaint; the approval process forced us to understand exactly what data leaves the environment, which turned out to be an important question to have answered before production.

Dynatrace works by deploying OneAgent on each node. It auto-discovers every process, injects instrumentation, and builds a topology map of services and their dependencies — what calls what, how often, with what latency and error rates. The Davis AI engine flags anomalies without requiring manual threshold configuration. For an environment with fifty-plus services, that auto-discovery is genuinely useful; manually configuring monitors for every service you care about doesn't scale.

The regulated-environment wrinkle: we configured data capture exclusions to prevent query parameter values and request body content from appearing in traces — the agent can capture query strings and bodies by default, and customer financial data in an APM vendor's SaaS is a category of exposure we weren't going to accept. The exclusion patterns live in a policy file version-controlled alongside the OneAgent configuration. Dynatrace gives you the observability; you have to supply the judgment about what the observability tool itself is allowed to see.
