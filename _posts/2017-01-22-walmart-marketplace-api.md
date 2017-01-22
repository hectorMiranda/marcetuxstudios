---
layout: post
title: "Walmart Marketplace API and what it gets wrong"
date: 2017-01-22
author: marcetux
tags: [walmart, marketplace, api, integration]
---
We added Walmart Marketplace as a channel this month, which means I spent the better
part of a week reading their API documentation and a fair amount of time reading between
its lines. Walmart's seller API is newer than Amazon MWS and in some ways cleaner — the
auth model is OAuth-based rather than MWS's aging signature scheme — but there are rough
edges that cost real time.

The feed model is how Walmart handles bulk operations: you POST a feed of items or
inventory updates and poll for the result with the feed ID. The concept is fine. The
implementation is that the error details live inside the feed result, not in the HTTP
response — a feed that fails validation comes back 200 OK. If your consumer just checks
HTTP status, you'll happily ACK the message while nothing actually updated on Walmart's
side. We caught this in staging, but only because we checked an item's live state
manually, not because the response told us.

The other thing: the sandbox environment doesn't fully match production behavior. Some
feed types process instantly in sandbox and queue for minutes in production. The delta
matters when you're building consumer logic that retries on pending status — your timing
assumptions are wrong from the start. The move I'd make again is integration-test
against production with a test seller account before cutting over real inventory.
