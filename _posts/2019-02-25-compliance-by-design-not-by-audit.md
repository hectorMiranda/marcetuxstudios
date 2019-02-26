---
layout: post
title: "Compliance by design not by audit"
date: 2019-02-25
author: marcetux
tags: [banking, compliance, security, architecture, process]
---
Banking compliance work taught me the difference between "we're compliant" and "we're designed so that non-compliance is hard." The first is a point-in-time assertion that erodes as the system evolves. The second is architecture — defaults, guardrails, and workflows that make the right thing the easy thing, so you're not relying on someone to remember.

The concrete expression of this: our new service template enforces mTLS and structured logging by default. A team that starts from the template gets audit-ready log correlation and mutual authentication without making any decisions; they'd have to actively remove it to not have it. Key Vault references are the only accepted pattern for secrets in pipeline templates. OpenAPI specs are validated against a ruleset that flags endpoints missing authentication declarations or response schemas. None of this prevents a determined team from going off-template, but it raises the cost of non-compliance above the cost of compliance.

The shift in mindset is treating architectural decisions as policy. The architects at this bank spent a lot of time writing PDFs describing how things should be built; I'm trying to shift toward encoding those decisions in the templates, linters, and pipeline gates that teams actually use. A PDF no one reads isn't policy — it's a disclaimer. A pipeline that fails if the OpenAPI spec has no security scheme is policy. Auditors respond much better to "here is the gate and here are the gate logs" than to "here is the document we wrote."
