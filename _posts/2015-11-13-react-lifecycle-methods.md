---
layout: post
title: "React lifecycle methods and what they're actually for"
date: 2015-11-13
author: marcetux
tags: [react, javascript, frontend, architecture]
---
The video player component needed to integrate with the browser's `HTMLMediaElement` API
— addEventListener for `timeupdate` and `ended`, controlling play/pause imperatively.
That's the case lifecycle methods exist for, and working through it clarified which
lifecycle method does what and why.

`componentDidMount` is the hook for anything that needs a real DOM node or external
setup: add event listeners, initialize a third-party library, start a timer, make a
data fetch. It runs once, after the component's DOM is in place. `componentWillUnmount`
is the cleanup: remove event listeners, cancel subscriptions, clear timers. These two
are the core pair for any component that touches the outside world.

`componentDidUpdate` is for responding to prop or state changes — re-initialize an
external thing when a key input changes. `shouldComponentUpdate` is the performance
escape hatch, returning false when you know the component doesn't need to re-render.
The trap I've seen people walk into: using `componentWillReceiveProps` to derive state
from props, which runs into sync timing issues and is almost always a sign that state
and props aren't cleanly separated. If you're reaching for that one, it's worth asking
whether the state should exist in the component at all. The lifecycle is not a bug;
misusing it is.
