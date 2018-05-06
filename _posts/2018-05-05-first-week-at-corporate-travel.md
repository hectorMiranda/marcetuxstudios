---
layout: post
title: "First week at Corporate Travel Management"
date: 2018-05-05
author: marcetux
tags: [career, architecture, enterprise, backend]
---
The first week at a new company at the architect level is an exercise in listening and
asking obvious questions without embarrassment. I spent most of Monday in back-to-back
architecture walkthroughs, nodding at acronyms I half-recognized, and taking notes I'd
decode later. By Friday I had the first real mental model of the system: a core booking
platform that talks to four GDS systems, a reporting tier that runs nightly jobs, and
a client integration layer where every corporate account has its own configuration
profile and sometimes its own data format.

The tech stack is more conservative than I expected and more sensible than I
initially judged it for being conservative. .NET Framework 4.6.2, SQL Server,
a few WCF services that handle EDI with airline partners, and a set of REST APIs
built in WebAPI 2 that the client-facing portals consume. No Kubernetes, no YAML.
On-prem infrastructure managed by an ops team that has been doing this for a decade
and knows where the bodies are. The constraint isn't ignorance — it's stability
requirements I don't fully understand yet.

My first project is a performance review of the reporting tier, which runs six-hour
jobs nightly and is increasingly bumping into the window before business opens. That
is a tractable problem with a clear success criterion, which is the best kind of
first project at a new place.
