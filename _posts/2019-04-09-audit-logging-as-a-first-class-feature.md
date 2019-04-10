---
layout: post
title: "Audit logging as a first-class feature"
date: 2019-04-09
author: marcetux
tags: [banking, compliance, logging, architecture, security]
---
Most services I've worked on treat logging as plumbing — something you add to see what's happening when something breaks. Banking changes that relationship. Audit logging is not diagnostic; it's evidentiary. Regulators and legal can subpoena it. It describes what happened to customer data, who authorized it, and when. The design requirements for audit logs are therefore different from the requirements for application logs.

The differences that matter: audit log entries are immutable once written. No update, no delete. They go to a separate sink with stricter access controls than the operational logs — write-only from the application perspective, and readable only by the compliance team and by automated tooling that generates reports. Every entry carries who did it (the authenticated principal, not just a service identity), what was done (normalized to a controlled vocabulary — `payment.initiated`, `account.viewed`, `limit.updated`), what was affected (resource type and ID), and the before/after state for mutations.

The implementation uses a dedicated audit log service that the other services call via a fire-and-forget async publish. The payment service does not write to the audit store directly; it publishes an `audit.event` to Service Bus and the audit log service consumes it and persists it to immutable storage. That decoupling means the audit log service can enforce retention, encryption at rest, and access controls without every team having to implement them. It also means a bug in the audit log service doesn't take down the payment service. The audit trail is treated as more precious than the application data. In a bank, that's not an exaggeration.
