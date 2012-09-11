---
layout: post
title: "Two-factor auth with Twilio and Google Authenticator"
date: 2012-09-10
author: marcetux
tags: [security, 2fa, twilio, totp, csharp]
---

Added two-factor authentication to a portal login this week, and it's one of those
features that's far less code than the security value it buys.

We support two paths. The low-friction one is **SMS**: generate a short code,
send it through Twilio's REST API, and verify what the user types. Twilio makes
the sending a one-line HTTP POST, which is almost suspicious for something that
ends in a text message on someone's phone.

The better one is **TOTP** — the Google Authenticator style rolling code. No
network round-trip at verification time: you share a secret once (as a QR code),
and both sides compute a 6-digit code from the secret and the current 30-second
time window. In C# it's an HMAC-SHA1 over the counter, truncated to six digits.
The whole RFC 6238 algorithm fits on a page.

The gotcha with TOTP is always **clock drift**. Accept the adjacent time windows
(±1 step) and you'll spare yourself a pile of "the code doesn't work" tickets from
people whose phones are a minute off.
