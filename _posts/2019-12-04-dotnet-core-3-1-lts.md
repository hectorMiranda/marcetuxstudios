---
layout: post
title: ".NET Core 3.1 LTS and why the support cycle matters"
date: 2019-12-04
author: marcetux
tags: [dotnet, aspnetcore, lts, architecture, devops]
---
.NET Core 3.1 dropped this week and it's the LTS release — three years of support. For a bank that moves slowly and deliberately on platform upgrades, LTS is the distinction between "this is the version we run" and "this is the version we're evaluating." We've been on 2.1 LTS, and 3.1 is the version I'll be recommending we standardize on in Q1 planning.

The 3.1 release is mostly a refinement of 3.0 — Blazor Server polish, gRPC improvements, and Razor component partial class support. The new-feature work is incremental rather than dramatic, which for an LTS release is exactly right. The support lifecycle is the feature. New services that start in 2020 on 3.1 LTS will have a supported runtime until December 2022, which aligns with the bank's usual rhythm of major platform decisions. The 3.0 release was a "watch and adopt" signal; 3.1 is the "standardize" signal.

The migration path from 2.1 is clear for most of what we run. The Worker Service migration we did in September was done against 3.0 and it'll target 3.1 without changes. The nullable reference types work needs to migrate with it, and the `System.Text.Json` evaluation is still pending for the services that need custom converters. The LTS release gives us the time to do those migrations correctly rather than under pressure from an end-of-support deadline. That planning room is the real value of picking LTS versions when you have a choice.
