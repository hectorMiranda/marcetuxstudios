---
layout: post
title: "Vagrant kills the it works on my machine excuse"
date: 2013-03-08
author: marcetux
tags: [devops, vagrant, virtualbox, tooling, workflow]
---
The new developer on our team spent half of Thursday setting up a local environment
that matched staging close enough to run the app without it blowing up over config
differences. That kind of afternoon is a solved problem, and we solved it this week:
Vagrant, a `Vagrantfile` in the repo, and a provisioning script that builds the box
from scratch.

Vagrant wraps VirtualBox (or VMware, but VirtualBox is free) and lets you describe
a dev environment as code. The `Vagrantfile` says which base box to start from, what
ports to forward, and what script to run on first boot. Ours runs the same Chocolatey
and PowerShell setup we use to provision staging. `vagrant up` on a fresh machine, wait
a few minutes, and the app is running inside a VM that matches the environment where it
has to work.

The value isn't just the setup time — it's the discipline. "It works on my machine" is
a statement about a snowflake machine nobody else has. When your machine is a Vagrantfile
checked into the repo, "my machine" and "the server" converge toward the same thing.
We're not all the way to identical, but we're a lot closer than we were Tuesday. The
`Vagrantfile` is two hundred lines; Thursday afternoon is now about fifteen minutes.
