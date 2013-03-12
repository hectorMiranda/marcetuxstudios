---
layout: post
title: "Docker showed up and I have questions"
date: 2013-03-11
author: marcetux
tags: [docker, devops, linux, containers]
---
Docker released this week — or rather, the project went public enough that people are
writing about it — and I spent an evening reading what it actually is. Short version:
Linux containers packaged behind a developer-friendly CLI and image format. Longer
version: it's Vagrant's idea, but instead of a VM you get a container that shares the
host kernel and starts in seconds instead of minutes.

The concept is appealing. An image is a layered filesystem snapshot: base OS, then your
runtime, then your app, each layer cached and reusable. Ship the image, and the
environment comes with it. No "the server has a different libxml2 version" surprises.
Containers are isolated processes that can't see each other's files or ports unless you
explicitly wire them. On paper it's cleaner than a VM for this problem.

My honest hesitation is that we're on Windows servers and Docker today is a Linux-only
story. I can run it in the Vagrant box for experiment, but production is IIS on Server
2008. So: interesting concept, one I'm going to keep watching, but not something that
solves my actual deployment today. The tooling also smells like "just released" in ways
that remind me of early Vagrant — promising but plan for sharp edges.
