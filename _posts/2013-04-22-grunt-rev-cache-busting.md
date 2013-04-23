---
layout: post
title: "Grunt rev and the asset fingerprinting problem"
date: 2013-04-22
author: marcetux
tags: [javascript, grunt, caching, frontend, build]
---
The CDN work (from February) taught me to cache aggressively and purge deliberately.
That advice applies to your own static assets too, and the mechanism is the same one
CDN-backed apps use: put a content hash in the filename so a changed file is a new URL
that every cache, including the browser's, treats as new content.

`grunt-rev` does this automatically. It reads each asset — `app.js`, `main.css`,
images — computes an MD5 of the file content, and renames the file with a hash prefix:
`app.8f3a2c.js`. Then `grunt-usemin` rewrites every `<script>` and `<link>` reference
in the HTML to point to the hashed name. The deployed HTML ships with cache-forever
URLs for every static asset.

The discipline this enforces is actually useful apart from caching: if you can give
every static file a one-year `max-age` with confidence, you've proven that the content
is correct before deploy. You can't fingerprint and then sneak a fix in place, because
sneaking the fix in place would change the hash and therefore the URL. Breaking the
association between URL and content has to be a deliberate, visible act — a new deploy.
That tighter contract between "file" and "URL" is good for your architecture, not just
for your cache hit ratio.
