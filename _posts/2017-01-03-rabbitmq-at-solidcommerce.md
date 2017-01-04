---
layout: post
title: "RabbitMQ and the marketplace order pipeline"
date: 2017-01-03
author: marcetux
tags: [rabbitmq, messaging, dotnet, architecture]
---
SolidCommerce lives and dies by order throughput. When Amazon pushes a spike — a flash
sale, Prime Day, a listing going viral — the backend has to absorb it without dropping
orders or blowing past API rate limits. We were doing this with a home-grown queue
backed by SQL Server, and by November it was the thing keeping me up at night.

We moved the order intake leg to RabbitMQ over the holidays, and the mental model shift
was worth documenting. It's not just "queue as a service" — the broker enforces the
contract between publisher and consumer. Exchanges route messages to queues by topic,
so Amazon orders, Walmart orders, and eBay orders fan into their own queues with their
own consumer pools and their own rate-limit budgets. If Walmart's API slows down, its
consumer backs off while Amazon keeps moving. The SQL queue didn't know the difference.

The wrinkle I hit immediately is durability. Messages have to survive a broker restart,
which means the queue and the message both need to be declared durable — forget one and
you find out during a deploy, not a drill. Dead-letter exchanges are the other thing I'd
do from day one next time: when a consumer throws, the message lands in a known place
for inspection rather than disappearing. Queue it right or don't queue it at all.
