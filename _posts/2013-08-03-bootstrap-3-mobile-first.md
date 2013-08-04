---
layout: post
title: "Bootstrap 3 and the mobile-first grid that actually means it"
date: 2013-08-03
author: marcetux
tags: [css, bootstrap, frontend, responsive, design]
---
Bootstrap 3 came out this week and I spent the long weekend reading what actually
changed versus 2.x. The headline is "mobile first," and for once that phrase means
something architectural rather than just a marketing positioning. The old Bootstrap
started from a desktop layout and used media queries to collapse it for small screens.
Bootstrap 3 starts from the small screen and uses media queries to expand it — the
opposite direction.

The consequence is the grid classes: `col-xs-*` for every screen size, `col-sm-*`
for small and up, `col-md-*` for medium and up. Stack-by-default on mobile, side-by-side
on wider screens, without any JavaScript. The migration from Bootstrap 2 is not a quick
find-and-replace — the grid classes changed, the icon set moved to Glyphicons and the
markup changed, and some components were renamed or restructured. It's a real upgrade
with real work.

For the internal dashboards I'm assessing whether the upgrade is worth doing now or at
the next "new tool" opportunity. The existing tools are functional and the dashboards
aren't primarily viewed on phones. My plan: Bootstrap 3 for every new tool starting
today; existing tools stay on 2.x unless they get a significant redesign anyway. The
direction is clearly correct — starting from mobile is the right assumption for a web
tool in 2013 — but a migration for its own sake isn't the right use of a sprint.
