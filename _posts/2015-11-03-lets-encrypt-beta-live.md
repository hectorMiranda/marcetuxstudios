---
layout: post
title: "Let's Encrypt public beta is live"
date: 2015-11-03
author: marcetux
tags: [tls, https, lets-encrypt, devops, security]
---
Let's Encrypt launched its public beta this week, which means automated free TLS
certificates are available to anyone who wants them, no waitlist. I've been waiting to
write the proper follow-up since the June announcement and the beta delivers exactly
what was promised: run the `certbot` client against your domain, prove control, receive
a cert, done. From nothing to a valid certificate on the Pi 2's Nginx in under five
minutes.

The `certbot` tool handles the ACME challenge automatically for Nginx — it drops a
temporary file, the CA verifies the domain controls it, the cert is issued and
configured. There's even a `--nginx` flag that modifies the Nginx config to redirect
HTTP to HTTPS. The renewable cron job is two lines in `/etc/cron.d`: `certbot renew`
once a week, Nginx reload afterward. The 90-day certs renew without anyone touching
anything.

I've put HTTPS on the home Grafana dashboard and the Pi-based Jenkins instance. Both
are private network only, and yes I know a self-signed cert would have worked too —
but the principle matters to me. The cost of HTTPS was always complexity and process,
not money. Let's Encrypt just eliminated the process. There's no reasonable argument
left for HTTP on any web interface I control. The first time I ran the command on the
bench Pi and got a valid cert without paying anyone or filing anything, it felt like
a small piece of infrastructure that was always broken got quietly fixed.
