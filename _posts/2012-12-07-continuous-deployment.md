---
layout: post
title: "From continuous integration to continuous deployment"
date: 2012-12-07
author: marcetux
tags: [devops, ci, deployment, octopus]
---
We have CI (Jenkins builds every push) and one-button deploys (Octopus). The gap
between them is where the fear still lives — somebody has to *choose* to push the
button, and so deploys cluster nervously before lunch and never on Fridays.

Continuous deployment is closing that gap: a green build that passes the test gate
promotes itself to staging automatically, and production is one approval away.
Deploys get *smaller and more frequent*, which is counterintuitive until you live
it — small changes are easy to reason about and easy to roll back, so shipping ten
tiny deploys is far less scary than one quarterly monster.

We're not auto-promoting to production yet; a portal that bills customers earns a
human approval. But everything up to that gate is automatic now, and the cultural
shift is real: "deploy" stopped being an event we scheduled and started being
something that just happens when the tests are green.
