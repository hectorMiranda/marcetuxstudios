---
layout: post
title: "Policy as code with OPA and what it changes about compliance"
date: 2020-07-22
author: marcetux
tags: [kubernetes, security, devops, architecture]
---
The bank has compliance requirements that translate into things like "no container
shall run as root" and "all images must come from the internal registry." Enforcing
those manually — through review and periodic audits — means the gap between what
the policy says and what's running in AKS is as wide as the time since the last
audit. Open Policy Agent with the Kubernetes admission controller webhook is the
thing that closes that gap to zero at deploy time.

OPA lets you write policy in Rego — a declarative language purpose-built for policy
evaluation. The admission webhook calls OPA before every `kubectl apply`; OPA
evaluates the incoming object against the policy and returns allow or deny. A pod
spec that tries to run as root gets rejected before it lands in the cluster. The
developer sees the denial in the CLI output, fixes the spec, and tries again. No
audit required; the cluster physically cannot be in a non-compliant state for those
policies.

Rego takes some learning — it's not immediately obvious to people coming from
procedural languages — but the policies themselves are readable once the logic
click. The organization also benefits from policies living in a git repository with
reviews and history. The compliance conversation shifts from "are we compliant?" to
"here is the commit that added or modified this policy." That's a different kind of
evidence, and for auditors it's more useful than a screenshot.
