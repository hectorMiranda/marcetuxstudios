---
layout: post
title: "Angular component architecture and the one-direction rule"
date: 2017-01-11
author: marcetux
tags: [angular, typescript, frontend, architecture]
---
We've been running Angular 2 in production since November, and the dashboard that felt
cleanest in code review is the one where I was strict about data flow. Properties go
in via `@Input`, events come out via `@Output`, and nothing crosses that line. The
components that bent the rule — shared mutable services, direct parent references — are
the ones I spent the most January mornings debugging.

The one-direction rule isn't Angular's idea; React people call it "props down, events
up" and it's been around. But Angular's decorator syntax makes it concrete: if you
can't decorate it `@Input` or `@Output`, it shouldn't be communication. The component
becomes a box with labeled ports, and the thing wiring boxes together is the only place
that understands the full picture. Tests drop to "does this component emit the right
event given this input," which is short, fast, and catches most regressions.

What I'm adjusting to is the discipline in templates. It's easy to reach into a child
component from the parent via a `#ref` and call methods on it directly — Angular lets
you — but that's the one-direction rule breaking down in disguise. If the parent needs
to tell the child something, it changes the `@Input`. If the child needs to tell the
parent something, it emits. The rest is a naming problem I've already solved.
