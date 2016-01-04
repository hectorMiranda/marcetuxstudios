---
layout: post
title: "Let's Encrypt out of beta and into production"
date: 2016-01-03
author: marcetux
tags: [tls, security, devops, https]
---
Let's Encrypt cleared public beta in December and I spent the first weekend of the year
getting it running on a side project box rather than trusting the process to a domain
that actually matters. The pitch is that obtaining a cert becomes a two-minute shell
command instead of a multi-day vendor dance. Having done that dance for years, I was
skeptical the experience could really be that smooth. It mostly is.

The ACME protocol is the clever bit: the client requests a cert, the CA issues a
challenge — drop a specific file under `/.well-known/acme-challenge/` — the client
satisfies it, and the cert arrives. The whole flow is automated and the cert renews the
same way. For a domain that sits behind nginx, `certbot --nginx` rewrites the vhost for
you. First cert to serving HTTPS was under five minutes, renewal is a cron line.

The one thing I'm not doing yet is pointing a JibJab domain at it — Let's Encrypt is
brand new and I want to watch the revocation story for a few months before putting it
in front of real traffic. But the free, automated, scriptable cert is clearly the right
end state for anything that speaks HTTP. The days of "we'll get HTTPS eventually when
the cert renewal comes up" are over.
