---
layout: post
title: "SCIM deprovision and the case for soft delete"
date: 2026-05-04
author: marcetux
tags: [identity, scim, provisioning, dotnet, data-modeling]
---
The SCIM deprovisioning flow has a subtlety that trips up most implementations: the spec distinguishes between *disabling* a user (setting `active: false`) and *deleting* a user (HTTP DELETE on the resource). These are operationally different events with different downstream implications, and conflating them — handling both as a hard delete — is an easy mistake that's expensive to undo.

At AmaWaterways, a deprovisioned travel agent still has booking history, commission records, and audit trail entries that need to remain intact and queryable. A hard delete removes the principal that those records reference, which either cascades into data loss or leaves orphaned foreign keys. The right answer is soft delete with a deprovisioned status: set `active: false`, record the deprovision timestamp and source, and preserve the record. Access tokens for the user stop working at the Auth0 layer because Auth0 checks the `active` claim on every token refresh; the record persists for data integrity.

The SCIM DELETE operation maps to a *permanent* removal — the upstream IdP is telling you the user no longer exists even as a historical record. Treat that as a rare event, probably requiring a manual override in your system, and default to the soft-delete path when `active: false` arrives via PUT or PATCH. Most enterprise IdPs send `active: false` on offboarding; DELETE is unusual. If you implement only hard delete, you'll eventually field a compliance request to reconstruct the history of an account that no longer exists in your database.
