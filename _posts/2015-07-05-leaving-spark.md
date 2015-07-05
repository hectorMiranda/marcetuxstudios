---
layout: post
title: "Leaving Spark Networks"
date: 2015-07-05
author: marcetux
tags: [career, meta, retrospective]
---
Last day at Spark Networks was Friday. A year and a half, and I'm leaving the stack in
better shape than I found it: the match pipeline is queue-based and decoupled, the deploy
process is Capistrano instead of hand-SSH, Elasticsearch replaced the ILIKE queries,
the Rails models shed the worst of their weight into service objects. The codebase is a
fair amount better than when I arrived, which is the baseline I aim for.

The Couchbase work was the most interesting technically — view indexes, N1QL in preview,
the data modeling tradeoffs that come with a document store where the structure is in
the application rather than the schema. It's a different way of thinking about data
and I don't regret the time I spent on it, even the debugging sessions that weren't fun
in the moment.

The online-dating domain has its own challenges — A/B testing match algorithms is
legitimately interesting statistics work — but I could feel the ceiling. JibJab is video,
consumer product, AWS at a scale that online dating doesn't touch. FFMPEG pipelines,
image processing for face-swap personalization, S3 at serious volume. That's the work
I wanted and it starts Monday. Culver City was the right move in every sense.
