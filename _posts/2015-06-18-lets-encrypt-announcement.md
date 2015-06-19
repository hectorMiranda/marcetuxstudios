---
layout: post
title: "Let's Encrypt beta and free TLS everywhere"
date: 2015-06-18
author: marcetux
tags: [tls, https, security, devops]
---
Let's Encrypt announced its closed beta this month and the proposition is exactly what
the industry has needed: free, automated, open TLS certificates. The price of a
certificate has never been the real barrier for serious production systems — the barrier
has been the process: CSR generation, waiting on a CA, installing the cert, calendar
reminder to renew in 90 days, repeat. Automating that process is worth more than making
it free.

The ACME protocol that Let's Encrypt is built on is the interesting part. The client
proves domain control to the CA automatically — either by placing a file at a known
HTTP path or by making a specific DNS record — and the CA issues the cert, all via an
API. No email, no web form, no credit card. Renewal is the same API call run by a cron
job. The 90-day cert lifetime is a feature rather than a downside: it forces automation,
which means the renewal process is tested every 90 days rather than once a year when
the renewal email arrives and you panic.

It's not in GA yet — the beta is limited — but the intent is clear: TLS for everything,
no financial or procedural barrier. The "HTTPS only for the sensitive pages" era is
ending. When the full launch happens, there's no reasonable argument left for serving
any page unencrypted.
