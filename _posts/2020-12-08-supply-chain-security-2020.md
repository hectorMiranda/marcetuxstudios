---
layout: post
title: "Supply chain security became real this year"
date: 2020-12-08
author: marcetux
tags: [security, devops, cicd, dependencies]
---
The SolarWinds incident landing in December is a gut punch, and not just because of
the scale. The attack vector — a compromised build pipeline that inserted malicious
code into a legitimately-signed update — is one of the threat models the security
community has been describing for years. It's different from a stolen password or a
misconfigured firewall; it's trust in the software delivery chain itself that was
exploited.

I've been building toward supply chain hygiene all year without using that framing.
Dependabot for dependency CVEs. Pinned base images rebuilt weekly. Container image
scanning with Trivy before any push. OPA admission policies that reject images from
non-internal registries. GitHub Actions environment gates for production secrets.
None of these prevent a SolarWinds-class attack on a vendor I actually trust and
update — that's a different and harder problem — but they close the surface area on
the things I control.

What I'm adding in the next month: signed commits for every engineer on the bank's
repositories, and Sigstore-style image signing once the tooling matures enough for
production use. The goal is an unbroken chain of custody from code commit to running
container, where every link is verified and logged. It's more process than I'd have
called reasonable in January. The year's news has recalibrated "reasonable."
