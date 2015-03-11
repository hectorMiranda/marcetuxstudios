---
layout: post
title: "React Native ships for iOS"
date: 2015-03-10
author: marcetux
tags: [react, ios, javascript, mobile]
---
React Native is out. Not a demo, an actual release — open source, iOS, with the Android
preview promised. I spent an evening getting the example app running in the simulator
and the bridge-to-UIKit claim from the January announcement holds: the rendered views
are native UIKit components, not WebViews, and the scroll performance and gesture
recognition feel like it.

The development experience is the other surprise. You run the packager, start the
simulator, make a change in the JavaScript source, hit Cmd-R in the simulator, and see
it — no compile cycle, no Xcode build. Hot reload for native UI. The Xcode project is
still there for the native modules and the build system, but you almost never touch it
for UI work. That removes the thing that always slowed me down with native development:
the feedback loop between a change and seeing it.

I'm not shipping a React Native app tomorrow. The ecosystem is young, native modules
for anything outside the standard library require writing Objective-C bridge code, and
the Android story is still a promise. But this is the first time I've looked at a mobile
framework and thought "yes, this is actually the abstraction that fits how I think about
UI." The component model travels. The same mental model I'm using in the browser — tree
of components, data flows down, events bubble up — works here.
