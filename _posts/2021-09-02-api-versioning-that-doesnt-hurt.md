---
layout: post
title: "API versioning that doesn't hurt consumers"
date: 2021-09-02
author: marcetux
tags: [api, architecture, design, versioning]
---
We introduced a second version of the accounts endpoint and did it the way most
teams do: a new URL prefix, `/v2/accounts`, and a deprecation notice on `/v1/`.
Two months later we have three consumers still on v1 and no plan for when v1 goes
away because the migration path requires changes on their side that don't fit their
sprint calendar. This is the standard versioning story and the standard versioning
pain.

The version-in-URL approach creates a parallel universe problem: every breaking
change creates a new universe that the API team must maintain indefinitely or
enforce a migration timeline that consumers always have reasons to slip. Header-based
versioning (`Accept-Version: 2021-09-01`) trades URL proliferation for invisibility
— the URL looks the same, the behavior changes by date. Neither approach is wrong;
both require a migration conversation. The conversation is the actual problem.

What changed our situation was documenting the sunset date in the OpenAPI spec using
the `deprecated: true` field and the `x-sunset-date` extension, then wiring a
deprecation warning header into every v1 response. Consumers that log headers see
the warning on every call; consumers that don't log headers get an email. The
mechanic isn't important; the channel that reaches the consumer at call time is.
Deprecation is not a notice you send once. It's a signal you embed in the protocol
until the version is gone.
