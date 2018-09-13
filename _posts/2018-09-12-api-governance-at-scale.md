---
layout: post
title: "API governance at scale is different from API design"
date: 2018-09-12
author: marcetux
tags: [api, governance, enterprise, openapi, architecture]
---
The architecture team maintains an API standards document that covers naming
conventions, error response shapes, pagination patterns, and versioning rules. My
first task was reviewing a design proposal from an application team that violated
about four of those standards, which is where I learned that governance without tooling
is a losing game at scale.

The team that submitted the proposal wasn't being careless — they'd designed a
sensible API without reading the 47-page standards document, which is a completely
predictable outcome. Expecting engineers to read and internalize a document before
designing anything is hoping the situation doesn't arise, not preventing it from
arising. The fix is to encode the standards in tooling: an OpenAPI linter that runs
in the CI pipeline, a template repository that new API projects start from with the
correct patterns already present, and a standards document short enough to read in ten
minutes because the details are handled by the linter.

The spectral.io linting tool runs against OpenAPI documents and reports violations
against custom rules you define. I spent a week writing the ruleset — one rule per
standard, with an error message that links to the rationale. The CI check fails and
tells you exactly what's wrong and where the standard came from. That's the governance
model that works: make compliance the easy path, not the informed choice.
