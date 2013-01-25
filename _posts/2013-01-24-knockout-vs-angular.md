---
layout: post
title: "Knockout or Angular for the dashboards"
date: 2013-01-24
author: marcetux
tags: [javascript, knockout, angular, frontend]
---
Decision time on the front end, and it's not the slam dunk the Angular hype implies.
Having now built real screens in both, here's the honest split.

**Knockout** does one thing — binding — and does it beautifully. Drop it onto an
existing server-rendered page and enhance the dynamic bits. Low commitment, easy to
add incrementally, nothing else changes. For our older portal pages that are 80%
static, it's still the right tool.

**Angular** wants to *own the page*. It's a framework, not a library — routing,
DI, components, the works. For a genuinely app-like screen built fresh, that
structure is worth it. For sprinkling interactivity on a server-rendered page, it's
a heavy hammer that fights your existing markup.

So: not one winner. New app-like screens go Angular; enhancing existing
server-rendered pages stays Knockout. The mistake would be picking one religion and
forcing every page into it. The honest answer to "which framework" is usually "for
which page."
