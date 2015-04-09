---
layout: post
title: "Rails concerns, a mixed verdict"
date: 2015-04-08
author: marcetux
tags: [ruby, rails, architecture, backend]
---
Rails 4 introduced `ActiveSupport::Concern` as a way to extract behavior from fat
models into mixins, and after three months of using them my verdict is: useful for
exactly the right problem and immediately misused for everything else.

The right problem is shared interface behavior — `Searchable`, `Softdeletable`,
`Subscribable` — where multiple models share an exact contract and the mixin captures
that contract cleanly. A `Searchable` concern that adds `pg_search_scope` and the
associated methods to any model that includes it is a good concern. The model gets a
capability; the concern owns the mechanism; there's a clear reason why those things
travel together.

The problem is using concerns as a split tool for fat models. If you have a 900-line
Member model and you extract 300 lines into `Member::Notifications`, you now have a 600-
line model and a 300-line concern that is exclusively about Member — not a reusable
contract, just the same thing in a different file. The model is still fat, just spread
across two files you have to read together. Service objects are the right fix for that
fat; concerns are the right fix for shared behavior. Both exist; they solve different
problems, and reaching for the one that's in the same folder doesn't help if it's the
wrong abstraction.
