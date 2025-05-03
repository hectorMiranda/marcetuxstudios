---
layout: post
title: "My agentic coding workflow after four months"
date: 2025-05-02
author: marcetux
tags: [ai, agents, workflow, consulting, tooling]
---
Four months of shipping agent-assisted work on real client engagements is enough to say
the workflow has stabilized. Not "the technology is mature" — the tooling still moves
every week — but my personal process for getting reliable output from it has stopped
changing. That's the useful thing to write down.

The workflow: I decompose the problem myself, write the test or acceptance criterion
before handing the task to an agent, let the agent implement, run the verification, and
review the delta rather than the output. That last part is the key discipline — I don't
read the full generated file, I read what changed. This keeps me focused on whether the
change is correct rather than whether the code is familiar. Unfamiliar-but-correct code
is fine. Familiar-but-wrong code is the thing that slips through when you read for
comfort.

The tool I reach for most on implementation tasks is a coding agent with file access and
terminal access in a sandboxed environment. The sandbox is important: an agent with
unrestricted filesystem and network access is an agent I'm babysitting, not one that's
helping me. Constrain it to the project directory, give it the ability to run tests,
take its outputs seriously. That's the whole system.
