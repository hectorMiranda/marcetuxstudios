---
layout: post
title: "RSpec shared examples for common behavior"
date: 2015-03-04
author: marcetux
tags: [ruby, rspec, testing, rails]
---
Three service objects in the matching pipeline share the same contract: they raise
`ArgumentError` on nil input, they return an object with a `.success?` method, and
they log to a standard key on failure. Testing this contract three times — copypasted
into each spec file — meant a contract change required editing three files, and the
inevitable happened: one file lagged.

RSpec's `shared_examples_for` is the obvious fix and I'd been ignoring it. You define
the expectation set once under a name, then `it_behaves_like 'a matching service'` in
each spec pulls the whole set in. The shared block runs with the subject or a parameter
you pass, so each service provides its own valid input and the shared examples exercise
the common contract. Change the contract in one place; all three specs see it.

The discipline is keeping shared examples narrow — behavioral contracts, not
implementation details. A shared example that asserts on specific SQL calls is too
fragile. One that asserts on the interface — raise on nil, return a result object,
log on failure — is stable. The test suite is documentation of what the system promises.
Shared examples are how you document promises that multiple components make without
saying the same thing three slightly different ways.
