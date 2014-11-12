---
layout: post
title: "AngularJS 1.3 is final and the form validation changes"
date: 2014-11-11
author: marcetux
tags: [javascript, angular, frontend, forms, validation]
---
AngularJS 1.3 shipped final this month and the team at the former Edgecast job has
been asking me about the upgrade. I have opinions based on the beta work from August,
and the form validation changes are the ones worth explaining carefully.

The `ngMessages` directive replaces the `ng-show` soup that 1.2 required for form
validation messages. In 1.2 you'd write an `ng-show` for every possible error state on
every field — `ng-show="form.email.$error.required"`, `ng-show="form.email.$error.email"`,
each conditionally showing a `<span>`. In 1.3 with `ngMessages`, you declare a single
`<div ng-messages="form.email.$error">` and put `<div ng-message="required">` and
`<div ng-message="email">` inside it. The directive shows the first matching message,
which is usually what you want — one error at a time per field.

The upgrade path from 1.2 is smooth. The breaking changes are minor — `$http` no longer
transforms requests by default in a few edge cases, and `$interpolate` behavior on
brackets changed slightly. Both are documented in the changelog and neither affected the
portal code when I helped with the test pass. The one-time binding from August, the
`ngMessages` form improvement, and better error handling from the `$http` service made
1.3 worth the upgrade for any 1.2 app.
