---
layout: post
title: "GDPR prep and what we actually had to change"
date: 2018-04-09
author: marcetux
tags: [gdpr, security, privacy, architecture, compliance]
---
May 25th is the GDPR deadline and we've been going through the exercise at Go RN for
the past month. The healthcare angle means we're already HIPAA-constrained, which
handles a lot of the data-minimization discipline, but GDPR adds its own requirements
around EU data subjects that don't map 1:1. The most concrete work ended up being
three things: a data inventory, a right-to-erasure flow, and a consent audit.

The data inventory is harder than it sounds because data is everywhere you were sloppy
about it — a development database with a copy of production users you forgot to
anonymize, a third-party analytics SDK you added in 2016 and never audited, log lines
that record a patient's full name alongside their action. Most of our concrete work
came from this audit rather than from architectural changes: anonymize dev data, strip
PII from log payloads, add a consent flag to the user record and gate data collection
on it.

The right-to-erasure flow is the architectural piece. "Delete me" in a healthcare
system means soft-deleting the user record, nulling PII columns, and having a process
that verifies the cascade. Audit logs stay — HIPAA requires them — but they get
stripped of identifiers beyond the record ID. The honest lesson from this exercise:
data hygiene you should have had anyway becomes a legal obligation, and the audit makes
you see exactly where you cut corners.
