---
layout: post
title: "CDN cost audit and what the cache-miss rate was hiding"
date: 2016-07-02
author: marcetux
tags: [cdn, performance, cost, aws, media]
---
The monthly CDN bill has been roughly flat for a year, which I assumed meant the caches
were working. I pulled the detailed access logs and ran the numbers properly: cache hit
ratio for the HLS segments was 87%, which sounds good until you account for the fact
that 13% of our traffic hitting origin was costing more than the entire edge bill. The
math on a media platform does not work the same way it does on a text API.

The culprit was TTL on the segments. I'd set them to a conservative 24 hours because I
was nervous about invalidation — if a rendition needed to be replaced, I didn't want a
stale segment cached for a week. But HLS segments for VOD content are immutable. Once a
segment exists and the manifest points to it, it never changes. A file named
`720p/seg042.ts` for clip `abc123` is the same bytes forever. The right TTL is 365 days,
not 24 hours. If I ever need to replace a rendition I change the clip ID, which changes
all the segment paths.

After the TTL bump, the edge hit ratio went to 97% and stayed there. Origin traffic
dropped by roughly 85%. The bill dropped proportionally. The lesson: cache-hit ratio is
a derived metric and it rewards investigation. "87% is good" and "that 13% is costing
as much as the 87%" are both true and only one of them matters. Look at absolute cost
per request, not just the ratio.
