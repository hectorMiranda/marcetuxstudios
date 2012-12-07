---
layout: post
title: "Unit testing with NUnit and Moq"
date: 2012-12-05
author: marcetux
tags: [csharp, testing, nunit, moq]
---
The dependency-injection cleanup from last month finally paid off: with classes
asking for interfaces in their constructors, they're testable, and I stopped putting
off real unit tests.

NUnit for the structure — `[Test]`, `[TestCase]` for data-driven cases, clear
asserts. Moq for the seams: when the class under test needs an `IReportStore`, I
hand it a mock that returns canned data and verify the interaction. `mock.Setup(s =>
s.Get(4821)).Returns(sample)` and the test never touches a database.

The discipline I'm holding myself to: test *behavior*, not implementation. A test
that asserts "given these readings, the rollup totals are X" survives a refactor. A
test that asserts "it called this private method in this order" breaks every time I
clean the code up, and a test that breaks on healthy refactors is a liability, not a
safety net. Mock the boundaries, assert the outcomes.

*Update: a runnable version is in `examples/2012/testing/RollupTests.cs` — mocked
store, behavior asserted, no database in sight.*
