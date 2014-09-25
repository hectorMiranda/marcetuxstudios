---
layout: post
title: "RSpec and testing Rails controllers"
date: 2014-09-24
author: marcetux
tags: [ruby, rails, rspec, testing, tdd]
---
I've written unit tests in NUnit and MSTest for years, and the RSpec syntax took about
a week to stop feeling like an obstacle and start feeling like a better way to express
what tests are actually about. The `describe`, `context`, and `it` structure forces you
to think about what you're testing and under what conditions, which turns the test file
into documentation that doesn't drift.

The controller spec pattern in Rails RSpec is concise enough that I want to write it down
before I forget the learning curve. A controller spec hits the action with a fake request,
asserts on the response status and body, and stubs out service calls using RSpec's
`allow(...).to receive` pattern. The test doesn't touch the database if the service is
properly mocked; it doesn't start a browser; it just exercises the controller logic in
isolation.

The debate in the Rails community is whether controller specs are worth the investment
now that feature specs (Capybara, full stack from the browser) and request specs (HTTP
round trip but no browser) cover the same ground at a higher level. The pragmatic answer
I arrived at: controller specs for anything with non-trivial logic — authorization
checks, conditional rendering, error-state handling. Request specs for the happy path
that exercises the whole stack. Don't test the Rails framework; test the decisions you
made on top of it.
