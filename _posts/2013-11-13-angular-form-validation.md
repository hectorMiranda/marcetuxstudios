---
layout: post
title: "Angular form validation and the error message problem"
date: 2013-11-13
author: marcetux
tags: [javascript, angular, forms, ux, frontend]
---
The new customer onboarding form has about twenty fields, and the first version showed
validation errors only on submit — which means the user fills out twenty fields, hits
submit, and sees a wall of red. Angular has a per-field validation model that makes
inline feedback natural, and this week I sat down and did it properly.

Angular tracks validity state on every form control: `$touched` (the user interacted
with it), `$dirty` (the value changed), `$valid`, and `$invalid`. The pattern I settled
on is show an error only when the field is `$touched && $invalid` — not before the user
has focused the field, so the form doesn't open pre-highlighted in red. Using `ng-show="
field.$touched && field.$error.required"` on an error message div is declarative enough
to be readable and specific enough to be useful.

The tricky case is fields that depend on each other — confirm-password must match
password — which Angular's built-in validators can't express. Angular lets you register
custom validators as directive-based attributes: the confirm-password directive watches
the password field via `$watch`, runs the comparison, and sets a custom key on
`$setValidity`. The form controller picks up the state and the error message responds
automatically. The result is inline, contextual validation that shows up when a user
finishes with a field, not when they give up and hit submit.
