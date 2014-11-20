---
layout: post
title: "ActiveRecord callbacks and the costs you inherit"
date: 2014-11-19
author: marcetux
tags: [ruby, rails, activerecord, architecture, testing]
---
The Spark codebase uses `after_create` and `after_save` callbacks extensively, which
is normal in Rails apps of this vintage. The pattern is convenient — define behavior
that happens automatically whenever a record changes — and it hides costs that show up
later as test complexity and unexpected side effects.

The problem I keep running into in tests is that creating a `User` for a test scenario
triggers the `after_create` callback chain: an email is queued, a recommendation-engine
job is enqueued to SQS, a counter cache is updated. Three things I didn't ask for, each
of which requires its own stub or the test suite touches live SQS. The callbacks make
model creation a side-effect-laden operation rather than a simple data creation step.

The pattern the team is moving toward is explicit service calls over implicit callbacks
for anything that involves network IO or complex computation. `after_create :enqueue_recommendation_job`
becomes `RecommendationService.new(user).enqueue_job` in the controller, explicitly
called, explicitly testable. The model stays a data model; the orchestration is in a
place that can be inspected and tested without creating a record. Callbacks are fine for
pure data operations — updating a counter cache — and a maintenance problem for anything
that reaches outside the database.
