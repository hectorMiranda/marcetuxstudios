---
layout: post
title: "Angular promise chains and error propagation"
date: 2014-02-25
author: marcetux
tags: [javascript, angular, promises, async, frontend]
---
The async code in the dashboard services has been a mix of nested callbacks and
half-wired promise chains, and when something fails deep in a chain the error either
silently disappears or surfaces somewhere completely unrelated. I spent this week
untangling that, and the main thing I learned is that Angular's `$q` promises have
straightforward rules that I was only pretending to follow.

A `.then(success, error)` handler that returns a value creates a new resolved promise;
one that returns `$q.reject()` creates a rejected promise; one that throws creates a
rejected promise. Those rules mean you can chain `.then` calls and errors propagate
down to the first handler that addresses them, rather than requiring an error handler
at every step. The anti-pattern I was using was putting empty error handlers in the
middle of chains, which caught rejections and turned them into resolved promises with
undefined values — swallowed errors with no logging.

The fix was simpler than I expected: let errors propagate, handle them at the top of
each chain or in a service-level error handler, and log them explicitly before
rethrowing. The code got shorter and the debugging got easier because errors now tell
you where they happened instead of appearing as mysteriously undefined data two steps
later.
