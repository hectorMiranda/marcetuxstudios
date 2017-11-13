---
layout: post
title: "Terraform for infrastructure as code at a startup"
date: 2017-11-12
author: marcetux
tags: [terraform, aws, infrastructure, devops]
---
When I took over the AWS account at Go RN, the infrastructure was clickops — every
resource created through the console, no audit trail of changes, no ability to reproduce
the environment if we had to start over. This is a time bomb at any scale. Terraform
is the answer and I've been converting the infrastructure over the last six weeks.

The process: import existing resources into Terraform state, write the configuration to
match what already exists, verify with `terraform plan` that the delta is zero, then
make future changes through the configuration. The hardest part is the import — not
technically, but psychologically. You're writing Terraform config to describe something
that already exists, which means you have to read the existing config and transcribe it
accurately. Any drift between the Terraform config and reality will surface as an
unintended change in the next `plan`.

The startup-specific thing about Terraform: don't build a module hierarchy before you
need it. The Terraform tutorials love module composition examples with three layers of
abstraction. Start flat — all resources in `main.tf` and `variables.tf` — and extract
a module when you find yourself copy-pasting a group of resources. We have one module
so far, for the ECS task + service pattern, because that's the one we repeat. The rest
is flat. The complexity of Terraform modules earns its keep when you have repeated
infrastructure patterns; before you have them, it's ceremony.
