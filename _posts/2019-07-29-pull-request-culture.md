---
layout: post
title: "Pull request culture worth having"
date: 2019-07-29
author: marcetux
tags: [process, teams, code-review, devops, architecture]
---
The bank's engineering culture came with a pull request review process I found surprisingly thin for an institution that files regulatory reports with regulators. Two approvals required; in practice one approver would read the code and the second would click approve because the first had already done the work. Risk concentration hidden inside a process that looked like distributed review.

The review culture I'm trying to build is based on what a review is actually for. It's not a gate to catch bugs — tests and linters do that better and faster. It's a knowledge transfer mechanism: after a review, at least one other person understands what changed, why, and what it might affect. That's what the approval is attesting to. An approval from someone who looked at the diff for thirty seconds and saw no obvious syntax errors attests to nothing useful.

The concrete changes we made: PR descriptions require a context section — not "changed X" but "changed X because Y, which means the on-call engineer should know Z." Review comments have a classification: blocking (must change), non-blocking (would be better this way but I'll approve without it), or informational (just FYI). The distinction means approvals mean something — the reviewer identified what's blocking, discussed it, and the PR author addressed it. Two real approvals from people who read the context and the code is more protection than ten rubber-stamps.
