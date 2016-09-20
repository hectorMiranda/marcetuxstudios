---
layout: post
title: "Let's Encrypt is adding wildcard certs and the implications"
date: 2016-09-19
author: marcetux
tags: [tls, lets-encrypt, security, devops, https]
---
The Let's Encrypt team announced they're working on wildcard certificate support — a
single cert for `*.example.com` that covers every subdomain rather than per-domain
certs issued individually. It won't ship until 2017 but the implications are worth
thinking about now. The main scenario where I've been hand-issuing individual certs is
microservices with distinct subdomains for internal traffic; a wildcard cert simplifies
that considerably.

The current workflow for internal subdomains involves running certbot per subdomain on
the relevant machine, keeping the renewal cron jobs synchronized, and occasionally
discovering that a new subdomain someone spun up last month doesn't have a cert. A
wildcard cert for the internal `*.internal.jibjab.com` domain (hypothetically) would
be issued once, stored centrally, distributed to the services that need it. Renewal
would be once, not per-service.

The DNS-01 challenge type is how wildcard will work — you prove domain ownership by
creating a DNS record rather than serving a file over HTTP. That means the automation
needs to touch your DNS provider's API, which is a new integration requirement. AWS
Route 53 has an API; the certbot Route 53 plugin exists. When wildcard support lands,
the multi-subdomain cert management problem becomes a single renewal job with a Route 53
API call. I'm not implementing it today, but I know exactly what I'll build when it
ships.
