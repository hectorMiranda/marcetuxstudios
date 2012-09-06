---
layout: post
title: "Hello from the edge"
date: 2012-09-05
author: marcetux
tags: [meta, rest, csharp, cdn]
---

I've been meaning to start writing things down for years. Every few months I solve
the same problem twice because I never wrote down how I solved it the first time.
So: a blog. Plain Markdown, a static generator, and no excuses.

A bit about where my head is these days. I spend my days on the portals and
reporting side of a content delivery network — the part customers actually log
into to see how much bandwidth they pushed and where. It sounds boring until you
realize the numbers have to be *right*, *fast*, and *always up*, and those three
pull against each other constantly.

The lesson I keep relearning: **reporting is a read problem, and read problems are
caching problems.** Customers don't need their bandwidth chart recomputed on every
page load. They need yesterday's totals to be stable and today's to be "close
enough, updating soon." Once you accept that, the architecture gets simpler — you
precompute and aggregate on a schedule, serve the rollups from a cache, and keep
the expensive queries off the hot path.

The other thing I've settled on: ship the same data in more than one shape. Our
API hands back the same aggregates as **CSV** for the spreadsheet crowd, and as
both **XML and JSON** for the integrators. JSON has clearly won for new work, but
plenty of enterprise clients still wire up XML, so content negotiation earns its
keep. In C# that's mostly a matter of letting the formatter follow the `Accept`
header instead of hard-coding one writer.

A few principles I'm starting this blog with:

- **Make the common read cheap.** Precompute, cache, and treat freshness as a
  product decision, not a technical default.
- **Be boring at the boundary.** A predictable REST contract beats a clever one.
- **Write it down.** Hence this.

More soon — probably about the deployment pipeline, because that's the thing
keeping me up at night this month.

— marcetux
