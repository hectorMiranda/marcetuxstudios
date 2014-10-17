---
layout: post
title: "Pagination in a Rails API without the whole object"
date: 2014-10-16
author: marcetux
tags: [ruby, rails, api, rest, postgresql, performance]
---
The match-discovery endpoint returns the first page of potential matches for a user,
and the original implementation loaded all matches from the database and paginated in
Ruby. On a user with thousands of potential matches that's a full table scan filtered
to a subset, with the database doing more work and Ruby discarding more records than
necessary. Database-side pagination is the fix that the ORM makes easy.

Rails with `kaminari` or `will_paginate` generates SQL with `LIMIT` and `OFFSET` clauses.
`LIMIT 20 OFFSET 60` returns rows 61–80 from the result set, which is cheap on the
first few pages. The known problem is that `OFFSET` gets slower as the page number grows
— database must skip the first 60 rows before returning 20, which means reading 80 rows
to deliver 20. On page 50 it's reading a thousand rows to deliver twenty.

For the match discovery feed, the alternative is cursor-based pagination: instead of
`OFFSET`, use a `WHERE id > last_seen_id LIMIT 20`. The query doesn't need to skip rows;
it seeks directly to the cursor position. The downside is that you can't jump to an
arbitrary page; you can only move forward from a cursor. For an infinite scroll feed
that's exactly the right trade. Infinite scroll implies forward-only navigation; cursor
pagination is forward-only. Match the pagination model to the navigation model.
