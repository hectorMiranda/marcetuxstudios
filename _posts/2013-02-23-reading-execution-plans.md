---
layout: post
title: "Reading execution plans before adding indexes"
date: 2013-02-23
author: marcetux
tags: [sqlserver, sql, performance, databases]
---
A reporting query went from fast to painful as the bandwidth tables grew, and my old
instinct would have been to add an index and hope. This time I made myself read the
execution plan first, and the plan told a different story than the one I'd have
guessed.

The thing to hunt for is the fat arrow and the scan. SQL Server's plan shows each
operator and how many rows flow between them; a thick line into a **clustered index
scan** means it's reading the whole table where it should be seeking a few rows. My
culprit wasn't a missing index at all — it was a `WHERE` clause wrapping a column in a
function, which makes any index on that column unusable. The query was *non-sargable*,
a great word for "you've handcuffed the optimizer." Rewriting the predicate to leave
the column bare let it seek, and the scan vanished without a single new index.

Where I did add an index, the plan's own missing-index hint pointed the way — though I
take those as a suggestion, not gospel, since each index you add is a write you slow
down. The lesson I keep relearning: the plan is the database telling you exactly what
it's doing. Adding indexes by feel is guessing; reading the plan is just listening.
