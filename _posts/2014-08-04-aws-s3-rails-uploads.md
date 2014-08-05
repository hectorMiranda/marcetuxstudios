---
layout: post
title: "S3 direct uploads from Rails without hitting the app server"
date: 2014-08-04
author: marcetux
tags: [aws, s3, ruby, rails, uploads]
---
Profile photo uploads at Spark were going through the Rails app server — browser
sends photo to Rails, Rails streams it to S3, Rails responds to the browser. That's
a slow path: large uploads block a Unicorn worker for the entire duration of the
S3 write, and Unicorn workers are a finite resource. A 2MB photo upload ties up a worker
for a second or more while the S3 write happens over the network.

The fix is S3 presigned URLs for direct upload. The browser asks Rails for an upload
URL, Rails generates a time-limited presigned URL from the S3 SDK, and the browser
uploads directly to S3 without the file ever touching the app server. The presigned URL
encodes the bucket, key, expiry, and an HMAC signature; S3 validates the signature and
rejects uploads that arrive after the expiry or with a wrong key.

The app server's involvement shrinks to two fast API calls: one to generate the URL, one
to record the S3 key after the upload completes. The Unicorn worker is free in
milliseconds instead of a second. For a photo that gets cropped and resized anyway, it
doesn't matter that the original went directly to S3; the processing job picks it up
from there. Move the work to where it's cheapest to do — in this case, the browser and
S3 speaking directly without a proxy.
