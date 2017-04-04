---
layout: post
title: "AWS Lambda for lightweight webhook receivers"
date: 2017-04-03
author: marcetux
tags: [lambda, aws, serverless, architecture]
---
The seller webhook receivers at SolidCommerce are a good fit for Lambda and I've been
meaning to move them there since the beginning of the year. The receivers do very little:
validate an HMAC signature, parse the payload, drop a message onto a RabbitMQ exchange
or an SQS queue, return 200. That's not a service that needs to be always-on behind a
load balancer; it's a function that needs to be available and fast, which is exactly
what Lambda offers.

The migration was straightforward. A .NET Core Lambda function, API Gateway in front of
it, SQS behind it. The function runs in under 200ms once warm, and for a webhook
receiver that might handle a few hundred calls per hour, the cost difference from a
dedicated EC2 instance is not meaningful — but the operational difference is: no
instances to patch, no capacity to provision, no monitoring to ensure the process
is alive.

The cold start on .NET Core Lambda is the honest caveat. For a webhook that gets called
every few minutes it's not a problem — the function stays warm. For something that gets
called once an hour, the first call after a cold period takes a second and a half longer
than subsequent ones. That's acceptable for a background webhook; it would not be
acceptable as a synchronous user-facing API. Serverless is a tool with a shape, and
matching the shape to the problem is the whole skill.
