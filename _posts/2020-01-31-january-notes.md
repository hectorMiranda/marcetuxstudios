---
layout: post
title: "January notes"
date: 2020-01-31
author: marcetux
tags: [meta, retrospective]
---
January came out more infrastructure-heavy than I expected. GitHub Actions replaced
the Jenkins conversation I was dreading; Terraform state backends turned a team
footgun into a boring solved problem; and I finally sat down with the Kubernetes
resource model long enough to understand why pods were evicting when I left numbers
blank. The pattern that kept repeating: the tool does exactly what you tell it, and
"I didn't tell it anything" is still an instruction.

The Pi cluster running k3s is the home-lab story of the month. It's small and
constrained enough that decisions I can just throw money at in Azure have to be
reasoned through — you can't add a node to get out of a scheduling mistake when the
cluster is three $35 computers under your desk. Good pressure to actually understand
the scheduler.

February's agenda: get the sensor stack fully migrated to the cluster, wire up
the OpenAPI contract workflow on the next greenfield service, and write about the
Key Vault access policy mistakes I'm already seeing now that managed identity is in
front of more teams. The infrastructure work is done when you stop noticing it;
I'm still noticing it.
