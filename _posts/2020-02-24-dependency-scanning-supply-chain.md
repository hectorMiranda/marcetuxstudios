---
layout: post
title: "Dependency scanning and starting to care about supply chain"
date: 2020-02-24
author: marcetux
tags: [security, devops, dotnet, cicd]
---
The bank's security team sent a report in January listing NuGet packages across our
projects with known CVEs. None critical, but the list was longer than it should have
been, and the honest answer to "how often do you update dependencies?" was "when
something breaks." That's not a security posture, it's hope.

GitHub's dependency graph and Dependabot are the low-friction answer I'd been meaning
to enable. Turn them on, configure the auto-PR settings, and you get pull requests that
bump individual package versions when vulnerabilities land. The PR runs through the
same pipeline as any other change — build, test, merge if green. The work shifts from
"audit and patch in a panic" to "review and merge on a quiet Tuesday." That cadence
change is the actual security improvement; the tooling is just what makes it cheap.

The subtlety is indirect dependencies. The package you `dotnet add` has its own
dependency tree, and a vuln two levels down is your problem too. The graph surfaces
those. The discipline I'm building: treat a high-severity Dependabot PR the same way
I treat a production outage — it's in the queue ahead of features until it's merged.
A dependency you depend on but don't manage is trust you haven't examined.
