---
layout: post
title: "Load testing a Rails API with Apache Bench and what the numbers mean"
date: 2014-12-03
author: marcetux
tags: [ruby, rails, performance, load-testing, devops]
---
The load test I promised myself in November ran this week against the match-discovery
endpoint. Apache Bench is unsophisticated but it's already installed on every Linux
machine and the output is honest: `ab -n 1000 -c 20 https://api.spark.internal/matches`
sends a thousand requests with twenty concurrent connections and tells you throughput,
percentile latencies, and where the distribution falls apart.

The p99 was the number I cared about. The mean response time at 20 concurrent
connections was 180ms — acceptable. The 99th percentile was 1400ms, which means one
in a hundred requests takes eight times longer than the median. That gap is the symptom;
the query log showed the cause. The match-discovery query has a dynamic `ORDER BY`
clause that can hit different indexes depending on the sort parameter, and a minority of
sort combinations were falling back to a sequential scan. Covering the two additional
sort columns with index additions dropped the p99 to 340ms.

The lesson is that mean latency is not performance. The user who experiences the p99
didn't have a 180ms experience; they had a 1400ms experience. For a dating app where
the first impression of the browse screen is the entire first impression, that user
bounced. Measure the full distribution. Optimize the tail. The mean is a convenient
fiction.
