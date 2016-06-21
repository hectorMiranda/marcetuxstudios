---
layout: post
title: "CORS is not a security layer and other hard lessons"
date: 2016-06-20
author: marcetux
tags: [cors, security, http, frontend, api]
---
The HLS manifest and segment endpoints went behind a CORS policy when we started
serving them cross-domain, and a conversation with a colleague revealed a common
misconception worth writing down: CORS is not the thing protecting your API from
unauthorized access. CORS is the browser enforcing a policy about which *origins* can
make cross-domain requests from JavaScript. It does nothing to stop a curl command,
a Python script, or a mobile app making the same request directly.

The mechanism: the browser sends an `Origin` header; the server replies with
`Access-Control-Allow-Origin` specifying which origins it accepts. The browser enforces
the rule — it refuses to give the response body to JavaScript from a disallowed origin.
But the server received the request. The full request. With all the side effects that
implies. If your POST endpoint does something based solely on the presence of a valid
session cookie, and you've opened CORS thinking "CORS will prevent unauthorized
JavaScript from calling this," you have a gap.

The actual protection for APIs is authentication and authorization: validate the token,
check the permission, do or don't do the thing. CORS controls which browsers can make
cross-domain requests from JavaScript, full stop. For the manifest endpoints the right
combination is signed URLs (authentication) plus a CORS policy that allows the player
domain (browser behavior). Both, not either. Layered correctly so each does what it
actually does.
