---
layout: post
title: "Building a taxonomy for marketplace feed errors"
date: 2017-05-08
author: marcetux
tags: [solidcommerce, marketplace, integration, reliability]
---
After a year of marketplace integrations, the support team sees the same error types
cycle through on rotation. A seller's product images aren't accessible from Amazon's
servers. A required attribute is missing for a specific category. A price is below
the channel's minimum. An ASIN that exists in our system has been delisted on the
marketplace side. Every one of these produces an error message, but the error messages
are in the channel's format, not ours, and the support team has to decode them.

The fix is a taxonomy: a curated mapping from raw channel error codes to structured
types we own. **DataError** — the seller's data is wrong and needs correction.
**AuthError** — credentials expired or permissions changed. **ChannelError** — the
marketplace rejected something for policy reasons. **TransientError** — timeout or
rate limit, safe to retry. The support team now sees "DataError: missing_required_attr
[brand] for category Apparel" instead of the raw Amazon XML, and they know immediately
whether to contact the seller, re-auth the account, or wait for the retry.

The engineering side is a parser per channel that normalizes errors into the taxonomy on
the way out of the API layer. The parsers are ugly because the raw errors are ugly, but
they're contained — and they're where we add new error types as we discover them. The
taxonomy is a living document in the codebase, not a spreadsheet someone will forget
to update.
