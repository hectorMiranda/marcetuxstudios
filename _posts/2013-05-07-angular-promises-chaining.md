---
layout: post
title: "AngularJS promises and chaining async calls cleanly"
date: 2013-05-07
author: marcetux
tags: [javascript, angular, promises, async, frontend]
---
The bandwidth summary view needed three API calls in sequence: load the customer record,
then load the billing period for that customer, then load bandwidth samples within the
period. The first implementation was nested callbacks — success handler inside success
handler inside success handler — which is the kind of code that looks simple until you
need to add error handling and suddenly you're wrapping every level in try-catch.

Angular's `$q` service implements promises, and the thing that clicked this week is
that `then()` returns a new promise. That means you can chain: `.then(loadBilling).then
(loadSamples).catch(handleError)`. Each step receives the resolved value of the
previous step, transforms it, and passes the result forward. The error handler at the
end catches a rejection from *any* step in the chain, in one place, instead of the
scattered error handling you get with nested callbacks.

The discipline is keeping the handlers small. The chain becomes a readable description
of the sequence — load customer, get period, get samples — and each handler does one
thing. When step two fails in production, the error handler knows which step threw
because the chain is explicit and the stack shows the promise step. Callback nesting
hides that; chained promises surface it. `$q.all()` for the case where two calls can
run in parallel is the natural next reach.
