---
layout: post
title: "ngRoute vs ui-router for nested views"
date: 2014-02-03
author: marcetux
tags: [javascript, angular, routing, frontend, spa]
---
The portal's URL routing has been `ngRoute` since the start, and it's been fine for
flat views. The problem is that flat views are running out. The customer-detail section
now has sub-sections — bandwidth, reports, settings — and `ngRoute` models each as a
separate top-level route that blows up the layout on every transition. What I really
want is a router that understands that the customer header stays put while only the
content region changes.

`ui-router` models the application as a hierarchy of states rather than a flat list of
URL patterns. Each state can have its own view, its own controller, and can be nested
inside a parent state. The customer detail state becomes a parent; bandwidth and reports
become children that share the parent's resolved customer data without re-fetching it.
The URL changes, the header stays, only the content region swaps — which is exactly what
a dashboard should do.

Migration wasn't painless. `ui-router` is more opinionated than `ngRoute` about how you
name things and how state parameters flow. I spent an afternoon untangling routes that
relied on the implicit `$routeParams` service, which doesn't exist in `ui-router` — the
replacement is `$stateParams`, and the parent state has to explicitly pass what the child
needs. Worth the work. The routing is now a tree that matches how the user thinks about
the app, not a list that matches how I added screens.
