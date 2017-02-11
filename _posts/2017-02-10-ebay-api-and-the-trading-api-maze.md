---
layout: post
title: "eBay's Trading API and finding a path through it"
date: 2017-02-10
author: marcetux
tags: [ebay, marketplace, api, integration]
---
Adding eBay as a marketplace channel means dealing with eBay's Trading API, which is
one of the older APIs in active use in commerce — SOAP, large XML payloads, and a
call structure that reflects how eBay's platform was built in 2000 rather than how
developers think about it today. The surface area is enormous: listing management,
inventory, orders, fulfillment, returns — each its own call family with its own quirks.

The strategy I landed on is to isolate every eBay call behind a thin gateway class that
handles auth token refresh, call throttling, and the XML serialization ceremony. Outside
that class, code sees a normal async method that returns a typed result or throws a
domain exception. Inside, the ugly XML and the token bookkeeping are contained. This is
boring to write and invaluable when eBay changes something in the response structure —
the blast radius is one class.

The part I underestimated: eBay listing fees and category requirements vary by site (US,
UK, DE are all separate endpoints with separate category trees). A seller's US and UK
listings are not the same API call with a flag — they're separate auth tokens, separate
category lookups, separate fee structures. Model that as a first-class concept from the
start. We didn't, and we paid for it in the revision.
