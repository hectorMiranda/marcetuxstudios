---
layout: post
title: "Azure Key Vault with managed identity and no secrets in config"
date: 2020-01-17
author: marcetux
tags: [azure, security, dotnet, architecture]
---
Connection strings in config files are the kind of thing I've always known was wrong
and always excused as "we'll clean it up later." Later arrived when our security team
flagged three connection strings in an appsettings.json that had made it into a
feature branch. No prod exposure, but close enough to be uncomfortable.

Azure Key Vault plus managed identity is the pattern that finally deletes the excuse.
A managed identity is an Azure AD identity automatically assigned to an App Service
or AKS pod — no client secret, no rotating password, no credential for a developer
to accidentally commit. The identity is granted access to the Key Vault at the
resource level in Azure, and the app just calls the SDK with `new
DefaultAzureCredential()`. Locally you authenticate through the Azure CLI. In
production the managed identity takes over. The same code, no config change between
environments.

The one thing to get right is the access policy scoping. I've seen Key Vaults where
every app in a resource group has read access to every secret, which trades "string
in a file" for "string behind a door everyone has a key to." Grant each identity the
minimum set — specific secrets, get and list only. The vault is as good as its access
policies, and access policies should fit on a sticky note.
