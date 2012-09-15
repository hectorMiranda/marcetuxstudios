---
layout: post
title: "Deploying .NET with Octopus"
date: 2012-09-15
author: marcetux
tags: [deployment, octopus, dotnet, devops]
---

Picking up where the Jenkins post left off: the green build now hands its artifact
to Octopus Deploy, and releasing has stopped being a hand-cramping ritual of
copying files to servers and editing config in place.

The model that clicked for me: **build once, deploy the same package everywhere.**
Jenkins produces a versioned NuGet package of the app. Octopus takes that exact
package and pushes it to each environment — test, staging, production — applying
the environment's own config values at deploy time. The bits that run in
production are byte-for-byte the bits you tested.

That last part is the whole game. The classic disaster is rebuilding per
environment, or worse, tweaking a `web.config` directly on a live box and then
forgetting you did. Octopus turns configuration into variables it substitutes
during deployment, so the differences between environments are *data*, written
down, not folklore living on one server.

It also gives you a deploy you can repeat and roll back. "Promote the release that's
in staging to production" is a button, not an afternoon. For a small team shipping
a portal, that's a quiet kind of luxury.
