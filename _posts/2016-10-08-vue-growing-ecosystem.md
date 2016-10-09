---
layout: post
title: "Vue 2.0 and an ecosystem that is catching up fast"
date: 2016-10-08
author: marcetux
tags: [vue, javascript, frontend, spa]
---
Vue 2.0 shipped in September and the release is a significant upgrade from the 1.x I
looked at in February. The virtual DOM rewrite brings performance on par with React;
the render function gives you the same escape hatch React has when you need programmatic
template generation; and the server-side rendering story, which was absent in 1.x, is
now a real thing. The ecosystem that was "thin" in February is catching up with Vuex
for state management and vue-router for navigation both having stable 2.0 releases.

What Vue 2.0 does better than Angular 2 for certain use cases: the template syntax is
closer to HTML, the single-file component model (`.vue` files with template, script,
and style in one file) requires a build step but the result is more compact than Angular
2's four-file component structure, and the initial render performance is fast. For teams
with strong HTML/CSS background who are adding JavaScript behavior rather than building
a JavaScript application, Vue's component model is a gentler on-ramp.

For my JibJab context: Angular 2 is the right choice for the internal tools I'm
building because the team is TypeScript-fluent and the Angular ecosystem fits our
complexity. But Vue 2.0 is now a credible option for projects where Angular feels like
too much structure. The "growing threat" narrative I hedged in February has earned itself.
