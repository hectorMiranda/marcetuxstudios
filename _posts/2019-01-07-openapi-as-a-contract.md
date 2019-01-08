---
layout: post
title: "OpenAPI as a contract not just documentation"
date: 2019-01-07
author: marcetux
tags: [openapi, api, architecture, integration, contracts]
---
Most teams I've seen treat OpenAPI as an output — you build the API, then Swashbuckle generates the spec, and you post it somewhere for consumers to read. At the bank I'm trying a different approach: the spec comes first, and the implementation is what has to match it. The difference sounds procedural but changes everything about how teams talk to each other.

When the spec is the contract, consumers can start building against a mock the moment the YAML is committed. We use the spec to generate client SDKs in the consuming teams' language and server stubs in ours. Drift between the spec and the actual service becomes a build failure, not a runtime surprise. For a regulated environment where "what did this API look like on day X" matters to auditors, version-controlling the spec file is about as close to a paper trail as you get without actually using paper.

The friction is up front: somebody has to write the spec before the code exists, which feels backwards. But that's also when design feedback costs least. A comment on a YAML diff is cheaper than a redesign six weeks into integration. I keep telling people the spec is the cheapest prototype you'll ever build. So far two teams have had the religion; two are still generating after the fact, which at least gives us the docs.
