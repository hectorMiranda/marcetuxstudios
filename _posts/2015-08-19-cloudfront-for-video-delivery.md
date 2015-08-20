---
layout: post
title: "CloudFront in front of S3 for video delivery"
date: 2015-08-19
author: marcetux
tags: [aws, cloudfront, s3, cdn, video]
---
Serving video directly from S3 works until it doesn't — the S3 request rate limits are
real, the latency from a single region is noticeable outside US-East, and signing URLs
for access control works but doesn't scale elegantly when the CDN can handle it closer
to the user. CloudFront in front of S3 addresses all three of these and the JibJab setup
is a useful reference for what a production configuration looks like.

The distribution points at the S3 bucket as an origin using an OAI (Origin Access
Identity) — a CloudFront-specific IAM principal that's the only thing allowed to read
the S3 bucket directly. This means users can't bypass CloudFront to hit S3; all traffic
goes through the CDN, cache hit ratios are real, and you control access via signed
CloudFront URLs rather than signed S3 URLs. The distinction matters: a signed S3 URL
expiry is enforced by S3 on every request; a signed CloudFront URL is enforced by the
edge, but the edge then caches the content and subsequent cache hits don't hit S3 at all.

The TTL configuration for video is deliberately long — 30 days for completed renders,
which don't change — with invalidations triggered by the rare case where a render is
re-run for quality reasons. Cache aggressively, invalidate deliberately. The S3 bill
is a fraction of what it would be at CloudFront cache-miss rate; the CloudFront bill is
the expected trade. The math works because video files are large and the cache hit ratio
for a holiday season's renders approaches 95%.
