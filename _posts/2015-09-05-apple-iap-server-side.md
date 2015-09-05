---
layout: post
title: "Apple in-app purchases from the server side"
date: 2015-09-05
author: marcetux
tags: [ios, apple, iap, payments, backend]
---
JibJab's iOS app sells premium content via Apple in-app purchase, and the server-side
receipt validation is the piece nobody enjoys building but everyone needs to get right.
Apple's model: the app handles the payment, the client receives a receipt, the server
validates that receipt with Apple's servers before delivering the purchased content.
If you trust the client's "yes I paid" without server-side validation, you'll be
delivering premium content to people who ask politely.

The validation endpoint is `https://sandbox.itunes.apple.com/verifyReceipt` in sandbox
and `https://buy.itunes.apple.com/verifyReceipt` in production. You POST the base64
receipt, Apple returns a status code and a decoded receipt JSON. Status 0 means valid;
anything else is an error or fraud signal. The response includes the product ID,
transaction ID, and quantity — compare against what the client claims to have purchased,
and only deliver what the verified receipt says.

The detail that trips people: Apple recommends first validating against the production
endpoint, and if you get status 21007, retry against the sandbox endpoint. A real user
in production always sends to production; a dev running against the sandbox who
accidentally hits the production endpoint gets 21007. Handling that redirect code server-
side means one validation path handles both environments without special-casing the
client's environment in the API call. Small thing to document; non-obvious to discover.
