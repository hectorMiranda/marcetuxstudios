---
layout: post
title: "Building a provisioning audit trail that tells the truth"
date: 2026-04-01
author: marcetux
tags: [identity, scim, auditing, dotnet, compliance]
---
Compliance requirements in travel and hospitality are not as intense as banking, but they're real — particularly around who provisioned a user, when, with what permissions, and from which upstream IdP. The audit trail question came up in a compliance review last month, and the answer we had was technically true but practically useless: "the logs have it somewhere." That somewhere was distributed across application logs, Auth0 audit events, and database change history, and assembling a complete picture for a single user's provisioning history required correlating three different systems by timestamp.

The fix is a provisioning event log that's a first-class entity in the database, not a byproduct of log aggregation. Every SCIM operation writes a record: the external provisioning request ID, the operation type, the tenant ID, the source IdP, the before-state and after-state as structured JSON, the outcome, and the timestamp. This isn't a general audit log that captures everything — it's specifically the identity provisioning surface, because that's what compliance needs and because a general audit log that captures everything is a search problem waiting to happen.

The discipline I applied was "write the compliance query first, then build the schema." The compliance question was: "For user X, show me every provisioning event, when it happened, what changed, and who initiated it." Starting from that query meant the schema has exactly the fields that question needs and nothing more. The result is a table that reads like a ledger — chronological, append-only, legible to a human who opens it in a database client — rather than a log that requires code to interpret.
