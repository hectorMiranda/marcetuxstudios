---
layout: post
title: "A no-drama Jenkins pipeline"
date: 2012-09-14
author: marcetux
tags: [ci, jenkins, dotnet, automation]
---

Set up Jenkins to build and test on every push this week. Nothing exotic — and
that's the point. The value of CI isn't the tool, it's removing the sentence "it
builds on my machine" from the team's vocabulary.

The job is boring by design: poll the repo, restore packages, run MSBuild, run the
test suite, publish the artifact. If anything goes red, Jenkins emails the person
whose commit broke it. The social effect is bigger than the technical one —
nobody wants to be the reason the build is red on the dashboard, so people start
running tests *before* pushing.

Two things that made it stick:

- **Fail fast and loud.** A broken build should be impossible to ignore. We put
  the status on a screen in the room.
- **Keep the build under ~10 minutes.** Past that, people stop waiting for it and
  start ignoring it, and you've lost the feedback loop you built it for.

Next step is wiring the green artifact straight into a deploy so a successful build
can become a release without someone copying files around by hand.
