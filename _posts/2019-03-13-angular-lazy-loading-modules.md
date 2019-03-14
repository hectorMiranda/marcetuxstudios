---
layout: post
title: "Angular lazy-loading modules and the initial bundle problem"
date: 2019-03-13
author: marcetux
tags: [angular, frontend, performance, typescript, spa]
---
The internal banking portal had grown to the point where the initial JavaScript bundle was large enough to feel slow on the typical enterprise laptop on a corporate network. The code was correct; it was just shipping everything at once. Angular's module system has had lazy loading for a while, and I finally made the time to wire it in properly.

The pattern: each major feature area — account overview, payments, reports, administration — becomes a feature module with its own route configuration. The top-level router loads these modules on demand using the `loadChildren` syntax. The first navigation to `/payments` fetches the payments module bundle; subsequent navigations use the cached copy. The Angular CLI code-splits the output into separate files per feature module automatically once you've expressed the route this way.

The result on this app was cutting the initial bundle from around 2.1 MB to about 380 KB. The rest loads as the user navigates. From the user's perspective: the login and account overview pages — which is what most people see most of the time — are fast. Less-visited sections like the admin console are slower on first visit, and nobody has complained because nobody visits the admin console urgently. You optimize for the common path. The less-visited module loading for the first time is a fine place to burn a few hundred milliseconds.
