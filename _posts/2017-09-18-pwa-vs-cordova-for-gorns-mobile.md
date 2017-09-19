---
layout: post
title: "PWA vs Cordova for a mobile-first healthcare app"
date: 2017-09-18
author: marcetux
tags: [pwa, cordova, mobile, angular]
---
Now that the mobile strategy is mine to own rather than just influence, I spent a week
looking seriously at Progressive Web Apps as an alternative to the Cordova shell we're
running. The answer I came to: we stay on Cordova for now, for specific reasons, and
I want to document them before the "just switch to a PWA" conversation happens again.

A PWA delivers a lot of what Cordova delivers — offline capability, home screen install,
push notifications — without the App Store distribution tax and the build toolchain
complexity. In a world where the app's user base was primarily Android, I'd take that
trade immediately; Android's PWA support is genuinely good. The problem is iOS. As of
iOS 11, Safari's PWA support is improving but still lacks reliable push notification
delivery and the Web Share API, both of which are critical for a shift-coordination
app that sends notifications when a facility posts an urgent shift. A nurse who misses
a shift opportunity because push didn't deliver on iOS is a real business problem.

The Cordova shell works today on both platforms. The plan is to build the web app in
a way that it runs identically in the Cordova shell and as a standalone PWA — same
Angular codebase, same service worker for offline, only the native API calls abstracted
behind an interface that has a Cordova implementation and a browser implementation. When
iOS catches up, switching to the PWA path is a deployment config change, not a
rewrite.
