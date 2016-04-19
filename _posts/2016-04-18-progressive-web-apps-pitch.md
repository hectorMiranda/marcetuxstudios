---
layout: post
title: "The Progressive Web App pitch and what it actually means"
date: 2016-04-18
author: marcetux
tags: [pwa, javascript, frontend, mobile, service-workers]
---
Google's been pushing the "Progressive Web App" term hard this year — I've seen the
Lighthouse announcement, the talks from Chrome team members, the posts about service
workers and Web App Manifests. The pitch is that a web app can behave like a native app:
offline support, home-screen installation, push notifications, without going through an
app store. For a company with a hybrid mobile strategy the appeal is obvious.

The key piece is the Service Worker: a JavaScript background thread that intercepts
network requests and can serve them from cache. When the network is unavailable, the
service worker can return a cached response; when the network comes back, it syncs.
The model is explicitly fetch-intercept-respond — the service worker sits between your
app and the network and handles the routing. The Web App Manifest is simpler: a JSON
file that tells the browser the app's name, icons, and preferred display mode, and
enables the "add to home screen" prompt.

At JibJab we're not shipping a PWA today — our delivery mechanism is App Store apps,
and the service worker spec is new enough that browser support is still catching up.
But I spent a weekend with a test page and a service worker and the offline story is
genuinely compelling. If the spec stabilizes and Safari gets on board, this becomes
the right default for content-first web apps. The term is marketing; the technology
underneath it is real and interesting.
