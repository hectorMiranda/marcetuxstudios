---
layout: post
title: "React alongside Ember, not instead of it"
date: 2015-09-02
author: marcetux
tags: [react, ember, javascript, frontend, migration]
---
The team at JibJab has been having the Ember-versus-React conversation for a few months,
and the decision we've reached is pragmatic: new components go React, existing Ember
stays Ember until there's a business reason to replace it. The migration is incremental
rather than a rewrite, which is the only kind of migration that survives contact with a
product roadmap.

Ember 1.x and React can coexist in the same page load because React components are
self-contained — you `ReactDOM.render(component, domNode)` into any DOM element, and
Ember can own everything around it. The seam is a DOM node that Ember leaves alone and
React takes over. It's not elegant but it works without a big-bang rewrite, and the
important thing is that the two component trees don't step on each other's state.

What's motivating the direction is not that Ember is bad — it's that React's component
model is simpler to reason about for our use case and the ecosystem is moving there. The
personalized video builder — the new feature the team is planning — will be built in
React from the start, which means the next substantial piece of the product is a
greenfield React project. The Ember code base has its own lifecycle now: maintain it
where it works, migrate it where it doesn't, don't maintain two of everything.
