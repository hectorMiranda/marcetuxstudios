---
layout: post
title: "WannaCry and what our patching discipline actually looked like"
date: 2017-05-03
author: marcetux
tags: [security, ops, windows, architecture]
---
The WannaCry ransomware hit on May 12th and the thing it exposed — not just at the
companies it hit, but across the industry — is that "we patch regularly" is a belief
many organizations hold about themselves without checking. The SMBv1 vulnerability
WannaCry exploits had a patch available since March. Two months is long enough for
a lot of organizations to have been somewhere between "scheduled" and "still planning."

We spent the week after the outbreak auditing what our actual patching timeline looked
like for Windows hosts, EC2 instances, and internal services. The answer was better
than some and worse than I thought. The AWS-managed services (RDS, ElastiCache) were
current; the EC2 instances running older .NET services were not — two of them were
behind by more than 60 days. Not because anyone decided not to patch; because no one
had a dashboard showing the lag, so no one knew.

The useful thing WannaCry forced: making the gap visible. AWS Systems Manager Patch
Manager gives you a compliance view by instance and patch baseline. Setting that up
took an afternoon and now the patching gap is a number we can see and own. The insight
is obvious in retrospect: you can't hold yourself accountable to a metric you can't
measure. The patch compliance number is now on the ops dashboard next to the channel
health grid from last month.
