---
layout: post
title: "Feature flags in a compliance environment"
date: 2019-05-21
author: marcetux
tags: [banking, compliance, feature-flags, devops, architecture]
---
Feature flags felt natural to me from the startup days — ship dark, enable for a percentage, roll back without a deploy. The bank needs the same capability but has an additional constraint: a feature that's present in the codebase but inactive still needs to be disclosed in certain regulatory filings if it touches regulated functionality. "The code is there but turned off" is not the same as "not implemented" for compliance purposes.

The practical outcome: we maintain a separate flag inventory document that lists every flag, what it gates, which regulatory domains it touches if any, and the planned activation date. The compliance team reviews the flag inventory on the same cadence as code reviews for compliance-sensitive features. It's more process than a startup would accept, but it means the compliance team never gets surprised by a feature going live that they didn't know about — because the flag is in the inventory long before the code ships.

For implementation we use Azure App Configuration for the flag values — centrally stored, audit-logged changes, environment-specific overrides. The application reads flags at startup with a short cache refresh interval. One flag per logical feature, evaluated at the relevant branch point in code, not scattered across ten places. The convention is a static class of well-named constants so you can grep the codebase for every reference to a flag and understand its full scope. Compliance-friendly feature flags are just feature flags with better record-keeping. The underlying pattern is the same.
