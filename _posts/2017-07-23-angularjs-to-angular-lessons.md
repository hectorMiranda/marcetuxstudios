---
layout: post
title: "Lessons from the AngularJS-to-Angular migration"
date: 2017-07-23
author: marcetux
tags: [angular, javascript, typescript, migration]
---
We have one legacy AngularJS 1.x application still in production at SolidCommerce —
the oldest seller dashboard, the one that predates my time there. The conversation about
migrating it to Angular (4, now) has been ongoing since Angular 2 launched, and I've
been the one pumping the brakes. Not because the migration isn't the right call, but
because a big-bang rewrite is a way to deliver nothing for a long time and then deliver
the same features you had before.

The strategy I advocated for is the Angular upgrade module — `UpgradeModule` —
which lets you run both frameworks in the same application during the transition. You
migrate components one at a time, running the Angular component inside the AngularJS
shell until enough of the shell is migrated to flip the wrapper. It's slower than a
rewrite, it's messier to look at, and it's the option that keeps delivering value to
users throughout.

The things I've learned so far from the hybrid phase: the shared services are the hard
part. AngularJS services injected into Angular components and Angular services injected
into AngularJS directives both work through the upgrade adapter, but the type safety is
thin in both directions. The discipline is to migrate services before the components
that depend on them. Start at the leaves of the dependency tree, not the roots, and
each migration makes the next one cheaper.
