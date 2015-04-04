---
layout: post
title: "CloudWatch alarms as a sanity baseline"
date: 2015-04-04
author: marcetux
tags: [aws, cloudwatch, monitoring, devops]
---
We had an SQS queue that started backing up over a weekend and nobody noticed until
Monday morning when the match recommendations were hours stale. The fix is embarrassing
in retrospect: CloudWatch had the metric, we just hadn't set an alarm on it. Two hours
on a Saturday setting up alarms and that particular incident can't repeat.

CloudWatch alarms are one of those things that feel like overhead until you've lived
through a silent failure. The queue depth alarm is the obvious one: if
`ApproximateNumberOfMessagesVisible` stays above N for two evaluation periods, page
someone. But once you're in the alarm configuration UI the pattern multiplies: EC2
CPU, RDS connections, Elastic Load Balancer 5xx rate. Each alarm is a small declaration
of "this metric going wrong is a problem" — and the accumulation of those declarations
is your monitoring policy.

The SNS integration is how the alarms actually reach you: alarm → SNS topic → email or
PagerDuty. One SNS topic for critical (wake someone up) and one for warning (Slack only)
gives you a triage policy without any extra infrastructure. The stack isn't monitored
just because CloudWatch is collecting metrics; it's monitored when there are alarms
defined and routed. Metrics without alarms are data nobody looks at.
