---
layout: post
title: "React state versus props, finding the line"
date: 2015-09-15
author: marcetux
tags: [react, javascript, frontend, spa]
---
Building the first genuinely new React components at JibJab and the recurring question
that makes a senior engineer earn their keep: this piece of data, does it live in state
or does it come from props? Getting this wrong produces components that are hard to
test, hard to reuse, and hard to reason about, and the right answer is almost always
more obvious after you've gotten it wrong once.

The principle I've settled on: props are inputs, state is whatever the component itself
manages and that nothing outside the component needs to know about. A video player's
currently-playing flag is state — the parent renders the player and doesn't need to know
whether playback is in progress unless it needs to react to it. If the parent needs to
react to it, the player exposes an `onPlaybackChange` callback prop and the parent
decides what to do with that event. State lives as low in the tree as possible.

The failure mode I've seen most: a big state object sitting in a parent component that
gets passed down as props through three layers of components that don't need it, just
to get it to the one leaf that does. That's the signal to extract the child into a
separate component that manages its own state, or to use a callback to let the leaf
inform the parent only when something actually happened. Push state down; lift events
up.
