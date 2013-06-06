---
layout: post
title: "PRISM surveillance and the HTTPS conversation we should be having"
date: 2013-06-05
author: marcetux
tags: [security, https, http, privacy, web]
---
The Guardian broke the Snowden/NSA PRISM story this week, and whatever your politics
about surveillance programs, the technical implication is hard to argue with: plaintext
HTTP between a user and a service is observable by entities that aren't the user or the
service. We've been treating HTTPS as something you add for login pages and payment
flows and nowhere else. That framing looks a lot less defensible this week.

The case for HTTPS everywhere is simpler than the usual "encryption is for criminals"
framing makes it sound. TLS doesn't just keep content private — it also guarantees
integrity. An HTTP response can be modified in transit by a man in the middle: inject
ads, inject tracking scripts, alter content, redirect to a different URL. HTTPS prevents
all of that. The user gets what the server sent. That property matters independent of
state-level surveillance.

Our portal is already HTTPS on the login flow. What I'm auditing now is which API
endpoints still serve over plain HTTP because "they don't carry PII." Bandwidth metrics,
customer lists, even API tokens in URL query strings on some older endpoints —
observable, modifiable, or leakable. Getting them onto TLS before this becomes a policy
memo instead of my own initiative seems like the right move. The certificate cost
argument is getting weaker too; I want to see what that landscape looks like in a year.
