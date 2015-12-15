---
layout: post
title: "React controlled inputs and form state"
date: 2015-12-14
author: marcetux
tags: [react, javascript, frontend, forms]
---
The video creation flow has a multi-step form — title, privacy, sharing options — and
I built the first version with uncontrolled inputs, reading values from the DOM at
submit time. It worked until there was a validation requirement that showed an error
inline as the user typed, at which point the DOM-read approach couldn't work: I needed
to know the field value on every keystroke, not just at submit.

React controlled inputs are the solution: the input's `value` is driven by component
state, and an `onChange` handler updates that state on every keystroke. The input is
"controlled" because React owns its value — the DOM reflects state, not the other way
around. The inline validation case is now trivial: check state on `onChange`, set an
error state, the error renders immediately. No ref, no DOM access, no timing issues.

The downside is that every keystroke triggers a render cycle, which can be a performance
concern for large or complex forms. In practice, React's reconciler is fast enough that
keystroke rendering is not perceptible unless the component tree is enormous. For a
four-field form, it's imperceptible. For a hundred-field form with heavy computation in
the render path, there are strategies — but those are problems I don't have. The default
is controlled inputs because they're the model that makes validation, dependent fields,
and submit state predictable. Reach for uncontrolled only when you have a concrete
reason, not because they're simpler to start with.
