---
layout: post
title: "Azure App Configuration for feature flags across services"
date: 2020-08-12
author: marcetux
tags: [azure, dotnet, architecture, devops]
---
Feature flags in a distributed system have a coordination problem: if each service
keeps its own flag values in appsettings.json, enabling a feature means deploying all
services that need to know about it simultaneously. That's backwards — the point of
a feature flag is to separate the deploy from the enable. Azure App Configuration is
the central store that makes the separation real.

The .NET integration is an `AddAzureAppConfiguration` call in host startup. The
SDK polls for changes on a configurable interval — I use a thirty-second sentinel key
pattern: the config refreshes only when a sentinel key's value changes, not on every
poll. That keeps API traffic low while changes propagate within half a minute. Feature
flags in App Configuration use the Microsoft.FeatureManagement library — check
`FeatureManager.IsEnabledAsync("NewPricingEngine")` and the flag value comes from the
central store, not from a local file.

The operational pattern that makes this work: enable a flag in the App Configuration
portal, watch it propagate across services in the next poll cycle, and roll it back
the same way if something is wrong. No deploy involved. The emergency rollback story
went from "redeploy the service with the flag removed" to "flip a checkbox in the
Azure portal." That's the right distribution of responsibility between infra and code.
