---
layout: post
title: "PostgreSQL schema design for healthcare shift data"
date: 2017-10-08
author: marcetux
tags: [postgresql, databases, schema, architecture]
---
The Go RN data model has a core shape that's simple to describe and complicated to
get right: a **Shift** belongs to a **Facility**, can be claimed by a **Nurse**, and
has a lifecycle — posted, claimed, checked-in, completed, disputed. The first pass
schema that existed when I arrived modeled this as one table with a status enum, which
works until the audit requirements arrive. Healthcare has audit requirements.

The change I made: a separate `shift_events` table that's append-only. Every state
transition creates an event row — who changed what, at what time, from what previous
state. The `shifts` table holds the current materialized state for query efficiency;
the events table holds the complete audit trail. This is the event sourcing pattern
applied at a single-table level, not the full CQRS event sourcing architecture, which
would be the right answer for a larger system and the wrong tool for a two-person team.

The audit trail paid off immediately. A facility disputed a check-in and the support
conversation was three minutes: pull the events, show the timestamp, the GPS coordinate
recorded at check-in, and the nurse's device ID. No dispute about what happened; just
the data. That's the value of the append-only event table — not the architecture
pattern, but the actual answer it gives you when someone says "prove what happened."
The data was always there; the schema just needed to make it easy to find.
