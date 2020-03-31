---
layout: post
title: "March notes"
date: 2020-03-30
author: marcetux
tags: [meta, retrospective, remote]
---
March was the month everything changed and the work didn't stop. Going fully remote
surfaced every process assumption that was actually "someone walks down the hall" in
disguise — on-call handoffs, deployment coordination, the casual status checks that
substituted for dashboards. Writing them down was overdue.

The technical work ran in parallel. Linkerd mTLS is in a namespace. Terraform modules
exist now instead of four copies of the same files. The Grafana dashboard is up and
showing me a ghost load on the idle circuit that I haven't traced yet. The Blazor
WebAssembly preview is solid enough to commit to for the next project.

Working from home has clarified what good infrastructure actually means: work that
continues without someone present to shepherd it. The pipelines, the IaC, the async
runbooks — all of it is more obviously valuable now than it was in an office. April
will be about tightening what we built in a hurry: real observability for the
distributed services, and probably finally writing about the Splunk dashboard that's
kept me sane on-call.
