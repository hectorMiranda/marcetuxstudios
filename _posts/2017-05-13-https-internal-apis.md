---
layout: post
title: "HTTPS for internal APIs is not optional anymore"
date: 2017-05-13
author: marcetux
tags: [security, https, tls, architecture]
---
WannaCry triggered a broader conversation at SolidCommerce about attack surface, and
one item that came up was internal API traffic running over HTTP inside the VPC. The
argument for allowing it was "it's inside our private network, nothing external can
see it." The counter-argument, which I made, is that "internal" and "trusted" are
different claims, and conflating them is how you go from "a host got compromised" to
"everything got compromised."

The immediate concern is not encryption — inter-VPC traffic is not routed over the
public internet. The concern is authentication and integrity. A service accepting HTTP
traffic inside the VPC can't verify that the caller is who they say they are without
mutual TLS or an equivalent. An HTTP request is trivially forgeable from any host on
the internal network. When the question is "what's the blast radius of a compromised
host," the answer is "the entire internal API surface" if everything speaks plain HTTP.

Let's Encrypt made external HTTPS cheap; for internal service-to-service we're using
self-signed certificates with a private CA managed in AWS Certificate Manager Private
CA. It's not free but it's auditable. The change landed for the two highest-sensitivity
internal APIs this month — the credential store and the billing service. The rest of
the internal services are scheduled. Getting this in front of the team before an
incident is the better argument than any after one.
