---
layout: post
title: "End-to-end testing Angular with Protractor"
date: 2013-09-10
author: marcetux
tags: [javascript, angular, testing, protractor, e2e]
---
There's a test tier above integration tests: tests that drive the actual browser against
the running application. Angular's community answer in 2013 is Protractor, built on
WebDriverJS and designed to understand Angular's digest cycle so it doesn't click a
button and then assert before Angular has processed the event.

Protractor wraps Selenium WebDriver. A test navigates to a URL, finds elements by
Angular-specific locators like `by.model('current.name')` or `by.binding('customer.id')`,
interacts with them, and asserts. The Angular-awareness means Protractor automatically
waits for `$http` requests to finish and for the digest loop to stabilize before
making assertions — the flakiness source that plagues vanilla Selenium tests against
Angular apps.

I wrote six E2E tests covering the main dashboard flows: login, load customer list,
open a customer, view bandwidth chart, navigate to billing, export a report. They run
against the integration environment via ChromeDriver in headed mode during development
and headless in CI. The suite takes two minutes to run, which is too slow for the watch
loop but fine as a pre-merge gate. The value isn't catching bugs unit tests can't — it's
confirming that the actual user interactions work, because a passing unit test on the
service and a passing integration test on the controller still can't tell you the
`ng-click` on the export button is wired correctly.
