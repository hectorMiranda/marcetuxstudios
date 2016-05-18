---
layout: post
title: "React at the margins of a non-React codebase"
date: 2016-05-17
author: marcetux
tags: [react, javascript, frontend, spa]
---
The JibJab front end predates my time here and is not a React codebase, and it's not
becoming one anytime soon. But we had a new component — a video clip selector with
enough state to be irritating to build in vanilla DOM code — and I proposed using React
for just that piece. No full-page rewrite, no new build pipeline, just mount a React
component into an existing DOM node and let it manage its own little slice.

The thing that makes this workable is React's `render` function: call `ReactDOM.render(
<ClipSelector ... />, document.getElementById('clip-selector'))` and React owns that
subtree. The rest of the page doesn't know React exists. The component manages its
internal state, talks to the server via `fetch`, and renders its own updates. Nothing
about the surrounding codebase changes.

The component took two days to build and works reliably. The mental model of "state
flows down through props, events flow up through callbacks" is clear once you've spent
a few evenings with it — clearer than the mutable-DOM imperative approach, especially
for something with multiple UI states (loading, empty, populated, selected). Using React
at the boundary of an existing codebase rather than treating it as an all-or-nothing
framework swap is the most pragmatic entry point. You get the good part without the
rewrite.
