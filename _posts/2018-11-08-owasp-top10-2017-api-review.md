---
layout: post
title: "Walking the 2017 OWASP Top 10 through a bank API"
date: 2018-11-08
author: marcetux
tags: [security, owasp, api, enterprise, banking]
---
The architecture team runs a quarterly security review and November's was the first
I led. We walked the 2017 OWASP Top 10 through the Account Information API — the
REST layer over the account management system — and the findings confirmed what I've
seen in most enterprise codebases: the structural vulnerabilities are usually absent,
and the subtle ones are usually present.

The structural ones are gone because the platform handles them. Injection is a
non-issue when your ORM is parameterizing queries and no one is concatenating SQL
strings. Broken authentication is handled by AAD and MSAL. XSS is a front-end
concern, and the back-end API returns JSON, not HTML. The platform encodes the
lessons from a decade of public incidents and you get them for free by using it
correctly.

What we actually found: **Broken Object Level Authorization** — A2, and consistently
the hardest to prevent. An endpoint was checking that the caller had a valid token,
but not that the account ID in the path parameter belonged to the caller's customer
context. A legitimate internal service could query any account by guessing the ID.
That's a policy rule missing from the authorization layer, not a library bug. The
fix is an explicit policy check at the resource boundary that validates ownership, and
it belongs in code, not in a deployment checklist someone will eventually forget to
run.
