---
layout: post
title: "Azure Active Directory and enterprise authentication patterns"
date: 2018-09-20
author: marcetux
tags: [azure, authentication, security, enterprise, oauth]
---
Every internal API at the bank authenticates through Azure Active Directory, which
gives you a battle-tested identity platform but also a set of concepts you have to
understand correctly to use it safely. The first week I spent as much time on the
MSAL documentation as on any feature code, because getting OAuth2 / OpenID Connect
wrong at a bank is not a configuration you want to discover through an audit.

The patterns that apply here: **service-to-service** calls use the OAuth2 client
credentials flow with a managed identity where possible — no secrets in config, the
Azure platform handles the token. A managed identity assigned to an App Service gets
an AAD token for any target resource you've granted it access to, and you never touch
a client secret. That's the right default. Client secrets are for cases where managed
identity isn't supported, and they should be in Key Vault and rotated on a schedule.

**User-delegated** access is where the complexity lives. The on-behalf-of flow lets
a middle tier call a downstream API using a token that represents the original user
rather than the service. It's the right model for audit trails — the downstream system
sees who initiated the action, not just which service made the call. The MSAL
implementation is a few dozen lines but the conceptual overhead is significant; I've
added an architecture decision record in the team repo explaining which flow applies
when.
