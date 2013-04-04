---
layout: post
title: "Angular UI-Router and nested views that actually make sense"
date: 2013-04-03
author: marcetux
tags: [javascript, angular, spa, routing, frontend]
---
The portal's dashboard is a nested thing: a top nav, a customer context that stays
loaded, and a detail pane that swaps between bandwidth, storage, and billing views.
Angular's built-in `ngRoute` is flat — one outlet, one level, one route matches one
template. For anything shaped like a real app it runs out quickly. UI-Router handles
the nesting, and it took about two hours to convert the existing routes and never look
back.

UI-Router's abstraction is **states** rather than URL patterns. A state has a name, a
parent, a URL fragment, a template, and a controller. The dashboard state is the
parent; bandwidth and storage are children that inherit the parent's resolved data.
The URL composes automatically from parent and child fragments. Each level of the
layout has its own named `ui-view` outlet, and the router fills in the right level
without the outer levels reloading.

The concrete payoff: switching from bandwidth to storage doesn't destroy and re-create
the customer header. The parent state stays alive; only the child view swaps. Data
fetched in the parent's resolve — the customer record, the account tier — doesn't get
re-requested. The router is doing what a bored developer would have hand-coded in event
listeners, and doing it consistently. The built-in router was fine for a demo; UI-Router
is sized for the actual shape of this application.
