---
layout: post
title: "IaC and the configuration drift you don't see"
date: 2021-05-24
author: marcetux
tags: [devops, azure, terraform, infrastructure]
---
Someone made a manual change to an Azure App Service configuration in the portal
during a late-night incident. The change fixed the immediate problem. Three weeks
later a Terraform apply reverted it and broke the same thing again. The on-call
engineer from that night was the only person who knew the change had been made, and
they'd moved to a different team. This is the scenario infrastructure-as-code is
supposed to prevent, and we ran it anyway.

The tool isn't the discipline. We have Terraform; we do not have a policy that
makes direct portal edits impermissible outside of a break-glass process with
mandatory follow-up. Those are different things. Terraform will faithfully manage
what you put in the code and also faithfully blow away what you put in the portal
if the code doesn't reflect it, and the next `plan` output is the warning — if
anyone reads the plan before applying it. In this case the plan went through a
review step that wasn't read carefully enough.

The fix is two-pronged: Azure Policy assignments that deny certain configuration
mutations to human identities in production, so the portal can't be used to make
changes Terraform doesn't know about, and a required human approval on any
`terraform apply` in production that shows the plan diff in the PR it's attached to.
The policy makes the unauthorized path impossible; the approval makes the authorized
path deliberate. Infrastructure drift is a process failure before it's a tool
failure.
