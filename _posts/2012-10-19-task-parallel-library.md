---
layout: post
title: "When to actually reach for the Task Parallel Library"
date: 2012-10-19
author: marcetux
tags: [csharp, tpl, parallelism, performance]
---

After the `await` post a teammate asked the right question: "so when do we use the
Task Parallel Library?" Worth writing down, because async and parallel get
conflated constantly.

`await` is for **I/O-bound** waiting — don't hold a thread hostage while the
network or disk does its thing. The TPL — `Parallel.For`, `Parallel.ForEach`,
`Task.Run` — is for **CPU-bound** work you want to spread across cores. Different
problems.

The bandwidth aggregation job is my real example. It crunches a lot of rows into
rollups, and that's CPU work. `Parallel.ForEach` over independent customer buckets
keeps every core busy instead of one core plodding through serially. The catch, as
always with parallelism, is **shared state** — the moment two tasks write the same
total you need a concurrent collection or a partition-then-merge approach, or
you've traded a slow correct answer for a fast wrong one.

Rule of thumb I gave him: if the bottleneck is *waiting*, await it. If the
bottleneck is *computing*, parallelize it. Don't parallelize a network call; you'll
just have more threads waiting.
