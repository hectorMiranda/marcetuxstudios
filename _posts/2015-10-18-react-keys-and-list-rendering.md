---
layout: post
title: "React keys and why list rendering gets them wrong"
date: 2015-10-18
author: marcetux
tags: [react, javascript, frontend, performance]
---
A bug in the video library: when you delete a video from a list and the list re-renders,
the wrong item's thumbnail briefly flashes before correcting. The root cause is missing
`key` props on the list items, which makes React's reconciler guess about which DOM node
corresponds to which list item after the array changes. It guesses wrong.

The `key` prop is React's mechanism for stable identity in dynamic lists. When the list
re-renders, React compares the new element list against the old one by key. If a key
exists in both, React updates the existing DOM node for that item. If a key is new, it
creates a new node. If a key is gone, it removes it. Without keys, React falls back to
index-based comparison — first element to first element — which is wrong when items are
removed, reordered, or inserted in the middle.

The rule is: key should be a stable, unique identifier from your data, not the array
index. `key={video.id}` is right; `key={index}` is wrong as soon as the array can
change. Array index as key produces exactly the thumbnail-flashing bug I saw — the index
is stable but the item isn't — and it's a subtle enough bug that it survives code review
because the code looks correct. The key is not a performance optimization; it's a
correctness requirement for dynamic lists. Performance is the bonus.
