---
layout: post
title: "Writing the runbooks you wish existed when you arrived"
date: 2017-09-04
author: marcetux
tags: [devops, documentation, process, knowledge]
---
My last month at SolidCommerce is essentially a controlled knowledge transfer, and the
discipline I'm applying is: write the runbook you wish had existed when you joined.
Not the architecture diagram that looks clean at 30,000 feet. The actual document a
person uses at 11pm when the Walmart feed processor is backed up and they're trying to
figure out why.

A useful runbook is specific about symptoms, specific about checks, and specific about
commands. "The feed processor might be stuck" is not a runbook. "If the `q.feeds.walmart`
queue depth exceeds 500 and isn't draining, check the consumer logs at
`/var/log/feedworker/` for `Error: unauthorized` — this means Walmart rotated the
seller credential and you need to re-fetch from the credential store following the
procedure in section 3" is a runbook. The difference is the person who wrote it had to
think about what they actually do, not what they think they do.

The other thing I'm doing: recording the decisions that aren't in the code. Why is the
Walmart consumer pool capped at 3 — not 5, not 10? Because Walmart's rate limit for
that seller tier is 3 concurrent feed submissions, and going higher gets the account
flagged. That's not in a comment; it's in the runbook. Code answers "what"; the runbook
answers "why this number." The difference between an organization that survives a key
departure and one that doesn't is usually whether anyone wrote the second one down.
