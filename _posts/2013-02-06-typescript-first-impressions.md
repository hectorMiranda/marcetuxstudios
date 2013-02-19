---
layout: post
title: "TypeScript, first impressions"
date: 2013-02-06
author: marcetux
tags: [javascript, typescript, frontend, tooling]
---
The Angular app is now big enough that "did I spell that property right" bugs only
show up at runtime, in the browser, three clicks deep. So I gave TypeScript a real
trial this week — Microsoft's typed layer over JavaScript, Anders Hejlsberg's latest
— and I'm cautiously into it.

The pitch is honest: it's JavaScript plus *optional* static types, and it compiles
down to readable JS you'd have written anyway. That "optional" matters — I can add
types to the gnarly service layer and leave a quick throwaway script as plain JS. The
editor goes from a text box to something that actually knows my code: rename a field
and the call sites light up, autocomplete on my own objects, a red squiggle the
moment I pass a string where a number goes.

The caution: it's young, the type definitions for third-party libraries like Angular
are community-maintained and patchy, and there's a new compile step in a front-end
build that already has Grunt, LESS, and concat in it. But catching a class of bug at
compile time instead of at customer-three-clicks-deep is the kind of trade I usually
take. Trialing it on the new service layer only, for now.

*Update, two weeks in: I wired the TypeScript compile into the Grunt build, emitting
source maps so the debugger shows `.ts` even though the browser runs minified JS — the
chain from the source-maps post actually works end to end. The community `angular.d.ts`
typings are still rough and I've had to patch a few, but typing the service layer alone
has already caught two property-name bugs before they reached the browser. Keeping it.*
