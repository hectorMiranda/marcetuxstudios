---
layout: post
title: "Let's Encrypt in production two months later"
date: 2016-03-09
author: marcetux
tags: [tls, lets-encrypt, security, devops, https]
---
In January I said I'd watch Let's Encrypt's revocation story for a couple of months
before putting it on a real domain. Two months later it's on a real domain. The
revocation event I was half-expecting hasn't happened; the renewal automation has
been running on the test box without intervention; and the ACME protocol is stable
enough that I trust it.

The production deployment is a small internal API service that was still running on
HTTP — embarrassing for something that takes credentials. Certbot's nginx plugin
handled the cert issuance and vhost rewrite in one command. The cert renews via a
cron job that runs certbot with `--quiet` twice a day; if the cert is more than 60
days from expiry it does nothing, and if renewal fails I get a logged error rather
than silent breakage. The 90-day cert lifetime means the automation runs frequently
enough that any failure gets caught quickly.

What I notice most is that the "we'll get HTTPS eventually" conversation at work is
now harder to have. The barrier was always "the cert costs money and takes a week to
provision." Let's Encrypt removes both. There is no longer a valid reason for an
internal service to speak HTTP. The only remaining excuses are inertia and familiarity,
which are the wrong reasons to leave credentials in plaintext on the wire.
