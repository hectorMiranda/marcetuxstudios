---
layout: post
title: "Architecture Decision Records and what remote work made them necessary"
date: 2020-08-17
author: marcetux
tags: [architecture, process, remote, documentation]
---
Five months into fully remote work, the decisions that got made in hallway
conversations are invisible to anyone who wasn't in the call. "Why is this service
using gRPC but that one is still JSON?" requires hunting through Teams chat history
or asking the right person. In an office, institutional memory lives partly in people
who are physically proximate. Remote, it evaporates unless you write it down.

An Architecture Decision Record is a short markdown file — typically in the repo it
applies to — that captures a decision with its context, the options considered, and the
rationale. The format is not the point; the discipline is. The template I've settled
on: title (ADR-NNN: chose X over Y), status (proposed/accepted/superseded), context
(one paragraph on the problem), decision (one paragraph on what we chose), consequences
(what it commits us to). That's it. The whole thing fits on one page.

The value accrues over months. When a new team member asks why we're not using
Cosmos DB, the ADR from January explains that we evaluated it, got stuck on the
consistency model mismatch with our transaction requirements, and chose SQL with
the specific caveats documented. The conversation starts from the written context
rather than requiring the person who was in the room. Remote teams need more
institutional memory encoded in artifacts because the hallways are gone.
