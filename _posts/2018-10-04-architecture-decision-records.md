---
layout: post
title: "Architecture decision records are documentation that survives"
date: 2018-10-04
author: marcetux
tags: [architecture, documentation, process, enterprise, adr]
---
The architecture team at City National Bank has been writing ADRs — Architecture
Decision Records — for about two years, and being six weeks into the job means I get
to read them as a newcomer rather than as someone who remembers the meeting. That
position is clarifying. About half of them are genuinely useful: they record the
decision, the context that made it reasonable, and the alternatives that were
considered and rejected. The other half record the decision and nothing else, which
is worse than useful because it implies the decision was obvious when it wasn't.

The format I'm adopting from Michael Nygard's original template: title, status,
context, decision, consequences. The **context** section is the one that atrophies —
it's easy to skip when the context feels obvious to the people in the room, and
invisible to everyone who wasn't there. The rule I enforce for myself: if I can't
explain why a reasonable person would have made the opposite choice, I haven't written
the context correctly.

The consequences section is where ADRs earn their keep. Recording that a decision
creates a specific obligation — "this means we own the identity mapping layer forever"
— prevents the drift where teams later make choices that conflict with a decision they
didn't know existed. An ADR that exists in the repo and is linked from the relevant
service README is findable. A Confluence page from 2016 is not.
