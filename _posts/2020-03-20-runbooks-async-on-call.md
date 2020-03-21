---
layout: post
title: "Writing runbooks that actually work without you in the room"
date: 2020-03-20
author: marcetux
tags: [devops, process, remote, oncall]
---
The distributed team forced a runbook audit. What we found: most of our "runbooks"
were headings with one-line descriptions and an implicit "ask Hector" footnote. Useful
when Hector is twenty feet away at 10 AM; not useful at 2 AM when the payment service
is bouncing and nobody wants to figure out where the Confluence page is.

A usable runbook starts with symptoms, not with procedure. The on-call engineer knows
what Splunk is showing; they need to know what that means and what to try first. So
the structure I've landed on: what does failure look like (metrics, log patterns,
alert names), what are the first three things to check, what are the fix paths in
order of least-risky, and who do you wake up if none of those work. That last entry
is the hardest to fill in because it forces you to admit the runbook doesn't cover
everything.

The test for a usable runbook is the same test as the Helm chart: can someone who
didn't write it execute the most common recovery path in the middle of the night with
no context from you? If the answer is no, keep writing. The goal is to make your
expertise unnecessary at the worst possible time. That's not modest; it's engineering.
