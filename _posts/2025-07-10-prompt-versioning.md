---
layout: post
title: "Versioning prompts like code"
date: 2025-07-10
author: marcetux
tags: [llm, prompts, platform, engineering, process]
---
The prompt for a production LLM feature is code. It's logic that determines behavior;
it has bugs; it can regress; it needs review before it ships. Treating it as a string
in a config file — or worse, a string in a database with no history — means you can't
answer "what changed" when the feature starts behaving differently. That question comes
up in every post-incident review for an LLM feature I've seen in the last year.

The minimum viable prompt versioning setup: prompts are plain-text files committed
to the main repository alongside the code that uses them. Every change to a prompt
goes through the same review and deployment process as a code change. The CI eval
runs against the new prompt before it merges. This sounds obvious and is still not
the default in most of the codebases I've seen. The default is a string in a config
or database with no review and no history.

The more mature pattern is a prompt registry with explicit version IDs, where the
application code references a prompt by version string rather than inline text. A
rollback is changing the version string. An experiment is deploying version B to a
percentage of traffic and comparing eval scores. Both of these require that the prompt
is a first-class artifact with a stable identifier, not an ad-hoc string. Start with
git history; add the registry when the team is large enough that PR review alone isn't
sufficient coordination.
