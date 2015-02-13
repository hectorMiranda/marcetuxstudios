---
layout: post
title: "Ruby service objects, keeping Rails models thin"
date: 2015-02-12
author: marcetux
tags: [ruby, rails, architecture, backend]
---
The `Member` model at Spark had quietly become the junk drawer. Profile update logic,
match-score calculations, notification triggering — all in the model because that's
where it started, and models are easy to add methods to. The file hit 800 lines and
stopped being anyone's favorite place to work.

The Rails service object pattern isn't a gem or a framework — it's just a plain Ruby
class in `app/services/` that does one job. `MatchScoreCalculator.call(member, candidates)`
takes inputs, returns a result, has no business touching ActiveRecord for anything
except the final write. The model shrinks to persistence and basic validations; the
service owns the domain logic. Controllers become thin again because they hand off to a
service and format the result, rather than orchestrating the whole operation themselves.

The test story is the big win. A service object is a Ruby class, so you instantiate it,
call it with test doubles, and assert on the return value — no database required, no
request context to stub. The model tests still need a test database; the service tests
run in milliseconds. Moving logic out of models and into services doesn't fix a design
problem you never noticed you had until the tests get faster and you realize how much
setup the old tests were carrying.
