---
layout: post
title: "Azure Kubernetes Service and what managed means in practice"
date: 2018-08-11
author: marcetux
tags: [azure, kubernetes, devops, containers, architecture]
---
We stood up an AKS cluster for a new reporting microservice and I spent time
understanding how "managed Kubernetes" actually differs from the GKE cluster I was
running at Go RN. The answer is: the control plane differences are smaller than the
vendor tooling differences. Kubernetes is Kubernetes; what varies is how you provision
it, how you hook into the cloud provider's networking and storage, and what the
support experience looks like when a node goes sideways.

AKS's managed-node-pool autoscaling is solid. The cluster-autoscaler adds nodes when
pods are pending and removes them when they're underutilized, and the integration with
Azure VM scale sets means new nodes come up with the right credentials and
configuration automatically. The node drain behavior on scale-down is correct — it
honors pod disruption budgets and respects the graceful termination period — which is
the thing that matters for a service handling long-running batch reports.

The thing I'd flag for a team new to AKS: the Azure CNI networking plugin is the
right choice if you need direct pod IPs in Azure networking (and you probably do for
service endpoints), but it requires pre-allocating IP space from your subnet. Plan
your subnet size before you create the cluster, not after, because changing it later
involves recreating the node pools. That's a fifteen-minute conversation upfront that
saves a painful migration.
