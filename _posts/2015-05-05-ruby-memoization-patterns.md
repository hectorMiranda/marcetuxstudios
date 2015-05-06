---
layout: post
title: "Ruby memoization patterns and where they hurt"
date: 2015-05-05
author: marcetux
tags: [ruby, rails, performance, backend]
---
The `||=` memoization pattern in Ruby is so natural-feeling that I've put it in places
I shouldn't have. `@result ||= expensive_computation` caches the result in an instance
variable the first time and returns it on subsequent calls — correct, useful, and a
source of confusion in tests when the same object persists across test cases.

The specific failure I walked into: a service object was memoizing a Couchbase result
that returned nil for members not yet indexed. `@profile ||= fetch_profile(id)` stores
nil on the first call — a legitimate result — and then never re-fetches on subsequent
calls in the same instance, because `nil ||= fetch()` always evaluates the right-hand
side (nil is falsy). So the second call with a now-indexed member still returned nil
because it never got past the cached nil. The fix is `defined?(@profile) ? @profile :
@profile = fetch_profile(id)` — memoize the result of the call even when it's nil.

The broader lesson: memoization is a correctness decision, not just a performance one.
Memoizing a method that can legitimately return nil or false requires the defined-form.
Memoizing in a class that gets reused across requests (controller instances, long-lived
workers) requires thinking about lifetime carefully. The pattern is two characters of
syntax hiding a fair amount of semantics.
