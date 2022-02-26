---
layout: post
title: "February notes"
date: 2022-02-25
author: marcetux
tags: [meta, retrospective, rust, casper]
---
February filled in some gaps from January. The contract upgrade model now makes sense
to me in a way it didn't before — it's versioned packages, not live patching, and that's
the right decision even if it's more verbose. The gRPC streaming work was satisfying in
the way that good interface design usually is: one request, a long-lived connection, a
clean stream of events. I want to go back and do more systems work that uses streaming
as the primary transport instead of bolting it on afterward.

Rust error handling with `Result` and `?` has gone from feeling clunky to feeling
correct. The thing I notice now is that when I'm back in C# or TypeScript for tooling
work, I'm more aware of the places where exceptions hide information that should be in
the function's return type. The Rust model doesn't travel, but the habit of asking
"what are this function's actual failure modes?" does.

The energy monitor at home is running and already earning its keep — I've identified
two appliances worth replacing based on the numbers, which will pay back the Pi and the
clamp sensors comfortably. Measure the thing; then you can do something about it. Same
lesson, same place I keep landing.
