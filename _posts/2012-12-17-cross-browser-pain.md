---
layout: post
title: "Still paying the IE tax"
date: 2012-12-17
author: marcetux
tags: [frontend, browsers, javascript, css]
---
A customer's whole org is on IE8 and our shiny new dashboard looks like it lost a
fight. The 2012 reality nobody puts in the conference talks: a big slice of
enterprise traffic is still old Internet Explorer, and you support it or you lose
the account.

The toolkit, for the record. **html5shiv** so IE8 even acknowledges
`<section>`/`<nav>` exist. **Respond.js** to fake media queries so the responsive
layout doesn't collapse to a single 240px column. Polyfills for the `Array` and
`JSON` methods you forgot aren't ancient-IE-native (`Array.forEach`, I'm looking at
you). And testing on a real VM, because IE's bugs don't reproduce anywhere else.

The discipline that keeps me sane: **progressive enhancement.** The page works as
server-rendered HTML first; the JavaScript and the fancy CSS are improvements layered
on top. When IE8 can't do the fancy parts, it falls back to something usable instead
of something broken. Build up from working, don't degrade down from broken.
