---
layout: post
title: "API design lessons from a year of churn"
date: 2018-12-06
author: marcetux
tags: [api, architecture, rest, openapi, retrospective]
---
Three different companies in one year means three different API contexts, which is
an uncomfortable way to learn but a fast one. Go RN: a mobile-first health-tech API
where the mobile client controlled the pace and GraphQL was the experiment. CTM: a
B2B integration layer where stability and predictability mattered more than
expressiveness. City National Bank: a governed enterprise API where the contract is
a legal and compliance instrument, not just a developer convenience.

The thing that held across all three: the API you design on day one will be versioned.
Not "might be" — will be, because requirements change and clients don't update
on your schedule. The teams that designed with versioning in mind from the start
were able to introduce v2 endpoints without a scramble; the ones that didn't had
to either break clients or maintain the original behavior in perpetuity. The
versioning strategy is a day-one decision, not a day-ninety scramble.

The thing that surprised me at the bank level: OpenAPI as a contract artifact, not
just documentation, changed the design conversation before implementation. When
the spec is the thing you present in a review and the review can result in changes,
you iterate on the design in minutes rather than in code refactoring weeks. I'll
carry the spec-first workflow into every greenfield project from here.
