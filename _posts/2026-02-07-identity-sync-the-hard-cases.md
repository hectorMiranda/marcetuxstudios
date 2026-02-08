---
layout: post
title: "Identity sync and the hard cases nobody documents"
date: 2026-02-07
author: marcetux
tags: [identity, scim, provisioning, sync, distributed-systems]
---
The happy path of identity sync is what every tutorial covers: create a user in the identity provider, it shows up in your system, the attributes match. That's maybe thirty percent of the actual implementation. The other seventy is what happens when the create call succeeds in the upstream system but the write to your database fails partway through; when the same user gets provisioned twice from two different enterprise IdP connections; when an attribute arrives with a value that's valid per the SCIM schema but violates a uniqueness constraint that only your system knows about.

The pattern that covers most of these is a provision log: before you apply any inbound SCIM operation, write a record of what you're about to do. If the operation succeeds, mark it complete. If it fails, you have a retry target. If the same operation arrives again — same source, same external ID, same change set — the log tells you it already happened and you return success without re-applying. This is not a new idea; it's the same outbox pattern that shows up in any messaging system. SCIM just rarely talks about it because the spec optimistically assumes reliable delivery.

The second hard case is the uniqueness conflict: a user provisioned from IdP A and an agent account created manually share the same email, and then IdP B tries to provision the same person from the agency system. There is no standard answer to this; every system has to define its own merge or collision policy. What I can say from building a few of these is that the policy needs to be explicit in code, not implicit in catch blocks. Handle the conflict deliberately, emit a structured event with enough context to diagnose it, and surface it somewhere a human can review. Silent merges are worse than visible conflicts.
