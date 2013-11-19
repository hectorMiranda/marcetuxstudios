---
layout: post
title: "Preparing the CDN edge for holiday traffic season"
date: 2013-11-18
author: marcetux
tags: [cdn, performance, caching, devops, http]
---
Mid-November is when we audit CDN edge configurations before the holiday traffic
peak. The portal's customer base includes e-commerce companies and media properties
that see three to five times their normal traffic between Thanksgiving and New Year,
and they need the CDN to be the thing that absorbs it, not the thing that forwards
it to an overwhelmed origin.

The audit has three passes. First: are the `Cache-Control` headers set correctly for
each content type? Static assets need long TTLs and the fingerprinted URLs from the
Grunt build. API responses need short TTLs and explicit `Vary` headers if the response
differs by `Accept-Language` or similar. The second pass: is origin shielding enabled
for all origins? Shielding routes edge-to-origin traffic through a single regional
"shield" PoP rather than every edge PoP fetching independently — one origin request
instead of twenty-five.

The third pass is the one I dread: finding the customer who's been making the "I set
a long TTL for everything" mistake. Long TTL on a product page with inventory that
changes during a flash sale means every customer sees stale "in stock" for the duration
of the TTL. The right answer is long TTL plus explicit purge on inventory change, not
a short TTL for everything. Getting that message across before Thanksgiving is easier
than getting it across at 11 PM when the flash sale is live and the inventory is wrong.
