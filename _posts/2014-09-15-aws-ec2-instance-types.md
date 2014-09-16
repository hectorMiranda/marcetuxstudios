---
layout: post
title: "Choosing EC2 instance types for a mixed workload"
date: 2014-09-15
author: marcetux
tags: [aws, ec2, infrastructure, devops, performance]
---
The Spark infrastructure runs on EC2, and this month I got involved in the instance type
conversation for the first time. We're growing into capacity limits and the question is
whether to scale up (larger instances) or scale out (more smaller instances), and which
instance family fits the workload.

The workload is mixed. The Rails API servers are CPU-bound during peak — a lot of JSON
serialization and ActiveRecord query work, not memory-intensive. The Couchbase nodes are
memory-bound — Couchbase is designed to serve hot data from RAM, and the primary tuning
knob is how much of the bucket fits in memory. The SQS workers are I/O-bound — they
spend most of their time waiting on S3 reads and writes.

The conclusion we reached: API servers run on compute-optimized instances (c3 family at
the time) because they need CPU; Couchbase nodes run on memory-optimized instances (r3
family) because they need RAM for the cache layer; workers run on burstable general
purpose (t2) because they're I/O-bound and don't peg a CPU. Right-sizing to workload
type rather than one instance type for everything cuts cost and improves performance.
The tooling to understand the profile — CloudWatch CPU metrics, memory agent, I/O
stats — is what tells you which family is right. Guess first; verify with data.
