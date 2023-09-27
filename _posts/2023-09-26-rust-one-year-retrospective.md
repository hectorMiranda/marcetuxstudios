---
layout: post
title: "Rust, one year in: a retrospective"
date: 2023-09-26
author: marcetux
tags: [rust, career, reflection, casper]
---
A year of writing production Rust is enough to have opinions that aren't beginner
impressions. The pain I expected — the borrow checker, lifetimes, the learning curve
— was real but front-loaded. The gains I didn't expect are the ones worth writing
down.

The borrow checker is the most honest code reviewer I've ever worked with. It catches
a class of bugs before compilation that C# and Python hand to you as a runtime
surprise: use after free, iterator invalidation, concurrent mutation. Once you stop
fighting it and start reading its objections as design feedback, the code quality
improves in ways that transfer to other languages. I write C# differently because of a
year of Rust. The discipline of thinking about who owns what, for how long, is not a
Rust-specific insight even though only Rust makes it a compile-time requirement.

The rough parts at scale: the compile time. A mid-size Rust project is slower to
compile than the equivalent C# or Go, and incremental compilation helps but doesn't
solve it. I started measuring compile time as a metric and restructuring crates to
parallelize better. The other rough part: the ecosystem is younger in the places
where you need libraries you didn't write. For gRPC and async networking, Tonic and
Tokio are excellent. For some domain-specific things, you're writing it yourself or
accepting a library with one maintainer. That's the trade for a younger ecosystem.
