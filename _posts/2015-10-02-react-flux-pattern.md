---
layout: post
title: "The Flux pattern for React app state"
date: 2015-10-02
author: marcetux
tags: [react, flux, javascript, frontend, architecture]
---
The video library feature grew to a point where `state` and `setState` inside individual
components stopped being adequate. State needed to be shared across components that
have no parent-child relationship — the selected video in the sidebar needed to update
the main panel and the action bar simultaneously. That's the problem Flux addresses.

Flux is more of a pattern than a framework: a unidirectional data flow where components
dispatch **actions**, a **store** holds the application state and responds to actions,
and components listen to the store and re-render when relevant state changes. No data
flows backward; no component mutates state directly. Everything that changes the
application state goes through an action, which is a plain object describing what
happened. The store handles it and notifies subscribers.

The implementation we're using is a minimal one with Flux's dispatcher and hand-written
stores — not Redux, which is still new, not a heavy framework. The dispatcher is a
simple pub-sub mechanism; stores are classes that register handlers for action types and
emit a change event. It's about 100 lines total of store machinery, and the result is
that you can look at the action log and understand every state transition that happened.
That debuggability is the concrete payoff. Unidirectional flow isn't an aesthetic choice;
it's a choice that makes state bugs findable.
