---
layout: post
title: "Signed URLs for gated video delivery"
date: 2016-03-24
author: marcetux
tags: [aws, cloudfront, cdn, media, security]
---
Not every video clip JibJab delivers is public. Premium content needs to be accessible
only to authenticated users and inaccessible to hotlinkers who grab the S3 URL. The
clean solution is CloudFront signed URLs: the CDN validates a signature before serving
the object, and the signature includes an expiration timestamp. A link that expires in
ten minutes is worthless to a hotlinker by the time it circulates.

The mechanics: you create a CloudFront key pair, store the private key on your servers,
and when an authenticated user requests a video you generate a signed URL with an
expiry on the order of the video duration plus some buffer. CloudFront validates the
signature with the matching public key, serves if valid, 403s if not. The origin S3
bucket has no public access — objects are only reachable through CloudFront. The CDN is
the gate; nothing bypasses it to the origin.

The tricky bit is key rotation. The private key that signs URLs needs to rotate
periodically and the old key needs to remain valid long enough for in-flight playback
sessions to finish. CloudFront supports up to two active key pairs per distribution,
which gives you the overlap window. We rotate every 90 days: add the new key, wait a
week, remove the old one. The rotation logic is a Lambda that fires on a schedule. It's
boring machinery, which is exactly what security machinery should be.
