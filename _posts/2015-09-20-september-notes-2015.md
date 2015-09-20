---
layout: post
title: "September notes, 2015"
date: 2015-09-20
author: marcetux
tags: [meta, retrospective]
---
September was the month the React-alongside-Ember strategy moved from conversation to
commits. The first new component in React is in production, coexisting with the Ember
shell around it, and the seam is uglier than I'd like but invisible to users. That's
acceptable for a migration that doesn't require a freeze on feature work.

Apple in-app purchase server-side validation is working and correctly routes sandbox
receipts to the sandbox endpoint without special-casing the client. The thing I keep
noticing about iOS infrastructure work: the documentation describes the behavior but
not the edge cases, and the edge cases are where the production bugs live. The sandbox
receipt retry is one of those.

The thumbnail pipeline is the piece I'm most satisfied with this month. Pre-computed
template timestamps for representative frames means the thumbnails are actually
representative rather than random stills, and the transactional update keeping video
URL and thumbnail URL in one write was a deliberate decision that took about five
minutes and has already prevented two potential inconsistency bugs. Small correctness
investments pay interest.
