---
layout: post
title: "N+1 queries in Rails and what eager loading actually does"
date: 2014-07-07
author: marcetux
tags: [ruby, rails, activerecord, databases, performance]
---
Three weeks in and the first performance investigation fell in my lap: a feed endpoint
that was making sixty-something database queries per request. The bullet gem's logs made
it obvious — one query to load the matches, then one query per match to load each user's
subscription tier, which is the classic N+1 pattern. Rails makes this easy to fall into
because the ORM hides the queries and the records feel like objects you're just calling
methods on.

The fix is `includes`: `Match.includes(:user => :subscription).where(...)` tells
ActiveRecord to load the matches and the associated users and subscriptions in two
queries — or one in some configurations — instead of N+1. The Bullet gem flags the
missing `includes` and suggests the fix, which makes it a cheap code review step once
it's integrated into the development environment.

The subtlety is that `includes` isn't always the right answer. If you're filtering on
the association's columns, you need `joins` instead of `includes`, and the difference
matters because `includes` loads all associated records into memory while `joins` keeps
them in SQL. Joining is faster when you don't need the associated records; including
is faster when you do. Reading the query log — `Rails.logger` in development mode
exposes the SQL — makes the difference visible immediately.
