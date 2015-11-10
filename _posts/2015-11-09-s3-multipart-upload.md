---
layout: post
title: "S3 multipart upload for large video files"
date: 2015-11-09
author: marcetux
tags: [aws, s3, video, upload, backend]
---
A new holiday video template runs longer than the previous catalog — around 90 seconds
rather than the usual 30 — which pushed the rendered output past the size where a single
S3 PUT is reliable. A 180 MB file upload on a connection that hiccups at minute two
means starting over. S3 multipart upload fixes this with parallel parts that survive
partial failures.

The multipart API has three steps: initiate a multipart upload to get an upload ID,
upload individual parts (minimum 5 MB each, maximum 10,000 parts) in parallel, and
complete the upload by assembling the parts on S3's side. Each part gets an ETag on
upload; the complete call passes the part numbers and ETags in order. If a part fails,
you retry just that part — the other parts are already on S3 and don't need to re-
upload.

The Ruby SDK wraps this into a streaming upload that handles the chunking automatically:
`object.upload_file(path, multipart_threshold: 15 * 1024 * 1024)` — any file above 15 MB
goes multipart automatically, parallel parts, retry on failure. I didn't have to write
the part management code. The threshold tuning is real though: too small and you're
making thousands of API calls for short videos; too large and failures in the final
part of a large file require retrying more data. We landed at 15 MB as a reasonable
middle for our video size distribution. Measure the distribution before picking a
threshold.
