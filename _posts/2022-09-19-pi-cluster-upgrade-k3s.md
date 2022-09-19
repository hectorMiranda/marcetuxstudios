---
layout: post
title: "Upgrading the home Pi cluster from k3s 1.22 to 1.24"
date: 2022-09-19
author: marcetux
tags: [kubernetes, k3s, raspberry-pi, homelab, devops]
---
The home k3s cluster — three Pi 4s and an old NUC as the control plane — had been
running on 1.22 for most of the year, and the upgrade to 1.24 was the thing I kept
postponing because the API removals in that version cycle were real. PodSecurityPolicy
was removed in 1.25 (not our jump, but previewing the pain), and several beta APIs we
were using moved to GA with schema changes. Doing the upgrade carefully took a Saturday
morning.

The k3s upgrade process is simpler than upstream Kubernetes: pull the new k3s binary
on the server node, restart, wait for the control plane to come back, then roll the
agent nodes one at a time. The anti-gravity of k3s is that it handles etcd (replaced
by the embedded SQLite datastore in small clusters) and the container runtime in one
binary, so there's less to coordinate. The API version changes require updating
manifests — `apps/v1` for deployments was already stable so that was fine; the RBAC
and networking manifests needed the beta prefixes removed.

The cluster runs the home monitoring stack: Prometheus, Grafana, InfluxDB, and a few
personal services. Keeping it on roughly current k3s is worth the periodic upgrade work
because the CVE history on old control plane versions gets alarming fast. The rule I've
landed on: upgrade within two minor versions of current, don't fall behind more than
that. Kubernetes has release cycles; so should the home lab.
