---
layout: post
title: "IAM least privilege is not just a compliance checkbox"
date: 2016-05-10
author: marcetux
tags: [aws, iam, security, devops, cloud]
---
The transcoding workers have an IAM role. For the first year of the project that role
had `s3:*` on the media bucket and `sqs:*` on the queue. It worked. It also meant a
compromised worker had full delete access to every object in the bucket, including the
delivery renditions that the CDN serves. Nobody told me to fix it; I fixed it because
I kept thinking about it.

The right policy for a worker that reads from SQS and reads/writes specific S3 prefixes
is narrow: `sqs:ReceiveMessage`, `sqs:DeleteMessage`, `sqs:ChangeMessageVisibility` on
the specific queue ARN; `s3:GetObject` on `uploads/*`, `s3:PutObject` on `transcode/*`
and `delivery/*`. Nothing else. An IAM policy is executable documentation of what the
service is supposed to do — if the policy doesn't allow `s3:DeleteObject`, neither a bug
nor an attacker can use that service to delete objects, regardless of what the code says.

The work to narrow a policy is an hour, not a sprint. The pattern that makes it easy:
add `CloudWatch` `PutMetricData` and nothing else for any service emitting metrics, add
`logs:CreateLogGroup`, `logs:CreateLogStream`, `logs:PutLogEvents` for anything logging,
and be deliberate about each additional permission. Start from zero and add, don't start
from `*` and subtract. The subtraction version always leaves something you meant to
remove.
