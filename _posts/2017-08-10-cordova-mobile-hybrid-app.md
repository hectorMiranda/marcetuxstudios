---
layout: post
title: "Cordova for a mobile hybrid app at a new project"
date: 2017-08-10
author: marcetux
tags: [cordova, mobile, javascript, angular]
---
A side project I've been involved with since spring is nearing the point where it needs
a mobile presence. The project — a staffing coordination tool for healthcare shift
workers — has a web frontend in Angular and a Node backend, and the mobile question
came down to native vs. hybrid. For a two-person team without iOS or Android experience,
hybrid is the answer that ships.

Cordova wraps a web app in a native shell, giving access to device APIs (camera,
notifications, GPS) through a JavaScript bridge, and distributes as a real app from the
App Store and Play Store. The tradeoff everyone knows: it's not going to win UI
performance benchmarks against a React Native or native app, but for a CRUD-heavy
coordination tool with lists, forms, and push notifications, the Angular app running in
a Cordova wrapper is indistinguishable from a basic native app to a user doing their
actual job.

The integration points that needed the most care: push notifications via Cordova Push
plugin, which wraps APNs and FCM; camera access for selfie-style clock-in verification;
and deep links that work both when the app is installed and when it isn't (falling back
to the web app). All three work and all three have edge cases that only surface on
actual hardware, not the simulator. The advice I'd give: test on the oldest supported
device you can find, not the newest one. The newest one will work fine.
