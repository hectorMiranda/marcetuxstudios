---
layout: post
title: "Q1 identity work in retrospect"
date: 2026-05-26
author: marcetux
tags: [meta, retrospective, identity, career, architecture]
---
Five months in at AmaWaterways, far enough to have a view worth writing down. The work I came in to do — stabilize the SCIM provisioning layer, clean up the Auth0 pipeline, push the token design toward something defensible — is mostly done in its first-pass form. The idempotency fixes are in production, the Rules-to-Actions migration landed in March, the provisioning audit log is live and already served its first compliance request. The things I was making notes about in January are committed code now.

What I've been thinking about more recently is the difference between fixing the immediate problems and designing the system so that class of problem is less likely to recur. The idempotency issues we had weren't a SCIM problem; they were a "no shared pattern for write idempotency across services" problem. The audit trail gap wasn't an oversight; it was a consequence of each service owning its own logging without a cross-cutting standard. Fixing individual instances is one kind of work; establishing the pattern so the next service gets it right by default is a different kind of work, and the second kind is what actually compounds.

The studio in May is finally exactly how I wanted it: the bench sensor is live, the rack is properly cooled and monitored, the observability stack is stable. The home lab and the day job are running the same mental models — idempotency, observability, explicit error handling — which is a happy convergence I didn't plan for but will take. The spring has been productive. On to Q2.
