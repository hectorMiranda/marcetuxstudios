---
layout: post
title: "Push notifications in production — what the docs leave out"
date: 2017-10-13
author: marcetux
tags: [mobile, cordova, notifications, reliability]
---
The Go RN app depends on push notifications in a way most consumer apps don't — a nurse
who doesn't receive a shift notification in time to respond can't claim the shift, which
is a real business impact. That changes how you think about push reliability from "nice
to have" to "must instrument and monitor."

The things the Cordova Push plugin documentation leaves out: delivery is not guaranteed,
and on iOS the system will silently discard notifications if the device has battery
saver on or the app is in the background for too long. On Android, OEM power management
layers (Huawei and Samsung are the main offenders) can kill background processes and
prevent delivery on devices that passed all your tests. The notification arrives at the
APNs/FCM gateway successfully — your server logs show "delivered" — and the device
never rings.

The mitigation: treat push as fire-and-forget, and build a pull fallback. Every time
the app opens or comes to foreground, it fetches pending shift opportunities regardless
of whether a push was received. The push is the fast path — it gets the nurse's
attention quickly. The foreground fetch is the reliable path — it doesn't miss shifts
that push dropped. Both paths update the same UI state; the app doesn't know or care
which one ran first. Push is a hint, not a delivery guarantee. Building on that
assumption is what makes the system actually reliable.
