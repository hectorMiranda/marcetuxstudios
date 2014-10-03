---
layout: post
title: "Service objects in Rails and rescuing fat models"
date: 2014-10-02
author: marcetux
tags: [ruby, rails, architecture, patterns, oop]
---
The User model at Spark accumulated business logic the way old Rails apps do — a method
for each feature gets added to the model, the model becomes authoritative for behavior
it shouldn't own, and after two years it's a five-hundred-line file where one section
handles billing and another handles photo processing and another handles recommendation
scoring. It's not wrong exactly, just impossible to test one concern without loading all
of them.

Service objects are the pattern the team uses to drain the model. A service object is a
plain Ruby class with one public method — usually `call` — that encapsulates one
business operation. The photo processing service takes a user and an S3 key, runs the
resize and thumbnail logic, and returns a result object. The controller calls the service,
the model doesn't know it exists, and the service has no ActiveRecord dependency unless
it needs to write something.

Testing improves immediately. A service test can hand the service a plain struct instead
of a full ActiveRecord model, run the logic, and assert on the return value. No database
required, no Rails environment to load, just the class and a fast unit test. The pattern
doesn't require a gem or a framework — it's just Ruby classes that do one thing.
The model gets thinner over time, not by deleting behavior, but by moving it somewhere
that actually owns it.
