---
layout: post
title: "Vagrant: a dev box you can throw away"
date: 2012-09-26
author: marcetux
tags: [vagrant, devops, virtualization, workflow]
---

New person joins, spends two days getting their machine to match everyone else's,
discovers their bug doesn't reproduce because their machine *doesn't* match. We've
all lived this. Vagrant is the first tool that's made me think the "works on my
machine" era might actually end.

The idea: a `Vagrantfile` checked into the repo describes a virtual machine —
which base box, how much RAM, which ports forward, and a provisioning script that
installs everything. `vagrant up` builds it; `vagrant destroy` throws it away. The
environment becomes **code you version**, not a wiki page you follow and get wrong.

The part I didn't expect to love: throwing the box away is *encouraged*. Did
something weird to your environment debugging an issue? `vagrant destroy && vagrant
up` and you're back to the known-good state in minutes. The VM is cattle, not a pet.

It's VirtualBox under the hood so it's not featherweight, and a full provision
isn't instant. But "clone the repo, `vagrant up`, start working" is a real promise
now, and that's worth a few minutes of boot.
