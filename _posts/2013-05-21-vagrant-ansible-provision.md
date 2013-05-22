---
layout: post
title: "Swapping the Vagrant shell script for an Ansible playbook"
date: 2013-05-21
author: marcetux
tags: [devops, vagrant, ansible, provisioning, tooling]
---
The Vagrant provisioning shell script from March had grown to two hundred lines and
developed the characteristic shell-script problem: it was fragile, it wasn't
idempotent, and it broke in ways that were hard to debug because line eighty-seven
failed silently and line eighty-eight assumed it had succeeded. The solution I kept
reading about was Ansible, and a rainy Saturday finally made me try it.

Ansible describes the desired state of a system as a YAML playbook. Instead of "run
this command," it says "ensure this package is installed," "ensure this directory
exists and is owned by this user," "ensure this service is running." The key word is
*ensure* — the tasks are idempotent by default. Run the playbook twice and the second
run changes nothing if the first run succeeded. That property makes provisioning
debuggable: run it, fix the failure, run again, and only the thing you changed is
affected.

The Vagrant integration is one line in the `Vagrantfile`: `config.vm.provision "ansible",
playbook: "provisioning/site.yml"`. The playbook replaces the shell script, and it reads
like documentation. A new developer can open `site.yml` and understand what the
environment requires without interpreting shell error codes. Two hundred lines of
defensive shell became sixty lines of clear intent, and it actually works right on the
first `vagrant up`.
