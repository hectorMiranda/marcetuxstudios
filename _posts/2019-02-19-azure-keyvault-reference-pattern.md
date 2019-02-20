---
layout: post
title: "Azure Key Vault references and the configuration problem"
date: 2019-02-19
author: marcetux
tags: [azure, security, configuration, secrets, banking]
---
Configuration that contains secrets is one of those problems teams solve wrong for years before they solve it right. Connection strings in `appsettings.json` committed to the repo, environment variables in deployment scripts that live on a shared drive — I've seen every flavor of this, and each one has a story that ends with an auditor's eyebrow raised.

Azure Key Vault with App Service references is the version of this I'm happy with. The pattern: secrets live in Key Vault, and the app setting value is `@Microsoft.KeyVault(SecretUri=...)` — a reference, not the secret itself. The App Service runtime resolves the reference at startup and injects the value as if it were a plain environment variable. The app code doesn't change. The pipeline doesn't handle the secret. The deploy artifact never contains credentials. The audit trail for "who accessed which secret when" lives in Key Vault's diagnostic logs.

The configuration for AKS workloads uses the CSI secrets store driver for the same idea: mount Key Vault secrets as files or environment variables in the pod, rotation happens without redeployment, and the secret never passes through the pipeline or gets stored in a Kubernetes Secret that some overprivileged process could read. The discipline is enforcing it: linting pipelines to reject any step that accepts a literal secret as a parameter, making the Key Vault path the only accepted pattern. Boring constraints applied early keep the auditor's eyebrow horizontal.
