---
layout: post
title: "Killing the N+1 query in ActiveRecord"
date: 2015-05-14
author: marcetux
tags: [ruby, rails, activerecord, postgresql, performance]
---
A page that renders a list of 25 match candidates was firing 26 queries: one to fetch
the candidates and one per candidate to load the profile photo URL. The Bullet gem
found it in about 30 seconds, which is the most productive 30 seconds I've had this
month. The fix took another five minutes. The ratio is embarrassing.

ActiveRecord's `includes` is the solution: `Member.where(age_range).includes(:photos)`
loads all the photos for the result set in one additional query, then satisfies
`member.photos` from the in-memory association without hitting the database again. One
query instead of 26. The SQL it generates is a `WHERE photos.member_id IN (1, 2, ...)`,
which is readable and fast with the right foreign key index.

The Bullet gem works by instrumenting ActiveRecord during the request and logging when
an association is loaded inside a loop without a preceding `includes`. It's a development-
only tool — add it to the development group, configure the notification style (Rails
log, browser alert, or Slack), let it run while you click through the app, and it finds
N+1 problems that code review misses because the problem is invisible in the code and
only appears at runtime. Every Rails project should have it; I wish I'd added it six
months ago.
