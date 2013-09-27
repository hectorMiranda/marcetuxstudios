---
layout: post
title: "Using git bisect to find the commit that broke things"
date: 2013-09-27
author: marcetux
tags: [git, debugging, workflow, devops]
---
A performance regression appeared in the reporting endpoint sometime in the last three
weeks — I noticed it in monitoring but there was no obvious commit that introduced it.
`git bisect` cut the debugging time from "read the last three weeks of commits" to
"run a script six times."

The command is a binary search over the commit history. You tell git a commit you know
is bad (`HEAD`) and a commit you know is good (three weeks ago). `git bisect start`,
`git bisect bad`, `git bisect good <sha>`. Git checks out the commit in the middle of
that range and waits for you to test it. If the regression is present, `git bisect bad`;
if not, `git bisect good`. Git bisects again, eight commits become four, four become
two, two become one. It reports the first bad commit.

The regression turned out to be an accidentally removed index — visible the moment
I looked at the commit — that a previous query optimization had relied on. Never would
have guessed it by reading commit messages. `git bisect` doesn't care what the commit
message says; it cares what the code does. The automation variant is `git bisect run
<script>` where the script exits 0 for good and non-zero for bad — with an automated
performance test as the script, the whole bisect runs without me touching the keyboard.
That's the thing to set up before the next regression.
