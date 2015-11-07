---
layout: post
title: "React PropTypes as a runtime contract"
date: 2015-11-06
author: marcetux
tags: [react, javascript, frontend, testing]
---
The video card component was receiving a `video` prop that was sometimes an object,
sometimes null, and sometimes an empty object depending on which loading state the store
was in. The component rendered different things in each case, only one of which was
correct, and the wrong ones produced no error — just wrong UI. PropTypes caught it
cleanly: a `PropTypes.shape({ id: PropTypes.number.isRequired })` on the `video` prop
started logging a console warning the moment a component received null.

React's PropTypes system is a runtime type check for development — declare the expected
shape and type of each prop, and React warns in the browser console when a component
receives something that doesn't match. No TypeScript required, no build-time check — it
runs in the browser during development and the warnings are specific about which
component, which prop, and what was wrong. The video card receiving null when it expects
an object is exactly the class of error PropTypes surfaces.

PropTypes don't run in production builds — there's a Babel plugin that strips them for
performance — so they're pure development tooling. The right way to think about them is
as executable documentation: the PropTypes declaration is a machine-readable spec for
what a component expects, and it tells you when reality diverges from the spec during
development. Useful to have before you discover the mismatch in a user's browser.
