---
layout: post
title: "A first look at React and what the virtual DOM thing means"
date: 2014-04-15
author: marcetux
tags: [javascript, react, frontend, spa, architecture]
---
React has been showing up in my reading often enough that I spent a Saturday with it.
Facebook open-sourced it last year and the initial reaction I saw was skepticism — HTML
in your JavaScript? JSX looked like a step backward. After a day with it, I think the
skepticism was aimed at the syntax and missed the actual idea.

The virtual DOM is the idea. Instead of telling the DOM what to change — "add this node,
remove that attribute, update this text" — you describe what the entire UI should look
like given the current data, and React figures out the minimal set of DOM operations to
get there. The reconciliation step means you write code that is correct for any state
without having to reason about transitions. Angular's two-way binding does something
similar but through a dirty-checking cycle that gets expensive as the number of watched
values grows. React's one-directional data flow sidesteps that.

I'm not switching off Angular for the portal — that would be a rewrite, not an
improvement. But for new, isolated UI pieces the component model is appealing. The
enforced one-way data flow makes the rendering predictable in a way that two-way binding
sometimes isn't. I'll keep watching it; the ecosystem is still forming and the tooling
is rough, but the model is sound.
