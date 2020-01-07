---
layout: post
title: "Terraform state and the Azure backend"
date: 2020-01-06
author: marcetux
tags: [terraform, iac, azure, devops]
---
We started doing real Terraform work late in 2019, and the first thing that bit us
was treating state as an afterthought. Local state files work fine until two people
run `terraform apply` from different machines and one of them nukes infrastructure
the other one created. The solution is obvious in retrospect: push state somewhere
central, lock it while a run is in progress.

Azure has a native backend for this — state goes into a blob in Azure Storage, and a
lease on the blob acts as the distributed lock. Setup is a handful of lines in the
backend block and a one-time `terraform init` with the new backend config. After that,
every apply reads the current state from the blob, acquires the lease, does its
work, and releases. Teammates running plans concurrently are told to wait rather
than silently racing.

The subtlety is bootstrapping: the storage account that holds Terraform state can't
itself be managed by Terraform, at least not without contortion. So the pattern is
a tiny bootstrap script — plain Azure CLI — that creates the resource group and
storage account, and then everything else is Terraform territory. A small ceremony
that avoids a bigger headache. Get the state backend right before the first real
resource and you never have to untangle it later.
