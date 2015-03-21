---
layout: post
title: "S3 presigned URLs for direct browser uploads"
date: 2015-03-21
author: marcetux
tags: [aws, s3, rails, backend, architecture]
---
Profile photo uploads went through the Rails server — browser to app to S3 — which was
fine at low volume and a bottleneck the moment we ran any kind of traffic event. The fix
is presigned URLs: the Rails server generates a signed S3 URL with a short TTL, hands it
to the browser, and the browser uploads directly to S3. The Rails process is never in
the data path.

The Rails side is a quick call to the AWS SDK — `bucket.object(key).presigned_url(:put,
content_type: 'image/jpeg', expires_in: 300)` — and the response is a URL the browser
can PUT to directly. The frontend gets the URL, sends the file to S3 with the
appropriate `Content-Type`, and notifies the Rails API that the upload is complete.
Rails then verifies the object exists in S3 before updating the profile record. The
signed URL has the policy baked in, so S3 enforces the content type and size limits
server-side; you can't sneak a zip file through by lying about the type.

The CORS configuration on the S3 bucket is the step that's easiest to forget: without
it the browser's PUT gets blocked by the same-origin policy and you get an opaque error.
Allow the POST/PUT method from your app's origin, set the allowed headers, done. Profile
photo uploads are now S3's problem, which is the right place for them. Push the work to
where it belongs.
