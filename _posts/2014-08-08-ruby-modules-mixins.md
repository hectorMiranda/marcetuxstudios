---
layout: post
title: "Ruby modules as mixins and where they earn their keep"
date: 2014-08-08
author: marcetux
tags: [ruby, rails, architecture, oop, patterns]
---
Two months of Ruby and the part that still surprises me is how much of the language's
sharing patterns run through modules rather than inheritance. Coming from C# where the
alternatives are inheritance, interfaces, and extension methods, modules feel like a
fourth option that does something different from all three.

A module mixed in with `include` contributes its methods to the including class as
instance methods. With `extend`, they become class methods. The result is horizontal
code sharing that doesn't imply "is-a" relationships. The Rails codebase at Spark uses
modules heavily for concerns — a `Subscribable` module that adds subscription-checking
methods to the `User` model, a `Geocodeable` module that adds location logic to models
that have a location. Each module handles one aspect; the class that includes multiple
modules composes them.

The place it breaks down is when modules start depending on each other's state, or when
you're not sure whether a method comes from the class or from a module two levels up
the include chain. The included-module stack can be opaque to a reader new to the
codebase. The discipline I use: modules should be self-contained and their dependencies
should be explicit — if a method in `Subscribable` needs the user's `email` field, that
dependency should be documented, not just assumed. A module that silently depends on
the including class's interface is a trap.
