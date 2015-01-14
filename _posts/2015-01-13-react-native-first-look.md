---
layout: post
title: "React Native, a first look before it ships"
date: 2015-01-13
author: marcetux
tags: [react, mobile, javascript, ios]
---
React Native was announced earlier this month, and the demo code Facebook showed is
doing something I hadn't expected: it's not a WebView wrapper. It renders actual native
iOS components from JavaScript, bridging React's virtual-DOM diffing model to UIKit.
That's a genuinely different approach from Cordova, which leans heavily on a WebView and
always felt like the web pretending to be native.

The model from the demo is a React component tree where `<View>`, `<Text>`, and
`<ScrollView>` are iOS views, not divs. JavaScript runs on a background thread and sends
layout instructions over a bridge to the main thread where UIKit lives. The bridge is
async and batched, so you don't block the UI for JS computation. It's clever — you get
to write in a language and component model you already know, and the output is native
widgets rather than a HTML5 shim.

I can't ship it yet — the preview is iOS-only and not public — but I'm watching closely.
The dating product has a mobile web experience that we're perpetually one sprint away
from converting to something better. If React Native delivers on the demo promise, it
changes that conversation from "hire an iOS dev" to "let the JS team take a run at it."
