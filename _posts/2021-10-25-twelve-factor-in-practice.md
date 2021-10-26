---
layout: post
title: "Twelve-factor apps and the three factors that actually bite you"
date: 2021-10-25
author: marcetux
tags: [devops, architecture, cloud, design]
---
The twelve-factor methodology is old enough now that most of its advice is
table-stakes in any cloud-native context — config in the environment, stateless
processes, port binding. Teams new to it learn it as a checklist. The checklist
is fine, but the factors that actually hurt you in production are three of them,
and they hurt disproportionately to how much attention they get in introductory
reading.

**Logs as streams**: if your application writes log files to the local filesystem,
you will have a container-restart problem and a multi-instance debugging problem.
The logs that matter are gone when the pod is gone, or they're scattered across
three instances. Write to stdout, collect centrally, always. Not eventually. Always.
We found a service still writing to `C:\logs` inside a container well into the
Kubernetes migration; the container had been crashing silently for a week.

**Disposability**: if your application takes more than five seconds to start and
fail gracefully in less than ten to shut down, you have a problem that will hurt
you during scaling events and rolling deployments. Both startup and shutdown paths
need the same quality of engineering attention as the hot path. They're not edge
cases; they're the failure-recovery cases, which is when you need them most.

**Dev/prod parity**: the distance between the developer's laptop and production is
where bugs live. Docker closed most of the gap; environment variables, secrets
management, and dependent service versions close the rest. The factor that remains
is the data-volume gap, and no methodology fully solves it.
