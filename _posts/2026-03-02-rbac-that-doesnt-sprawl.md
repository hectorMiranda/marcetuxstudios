---
layout: post
title: "RBAC that does not sprawl"
date: 2026-03-02
author: marcetux
tags: [identity, rbac, dotnet, authorization, architecture]
---
Role-based access control is one of those systems that starts clean and drifts toward chaos if nobody enforces a boundary on it. I've seen it happen a few times now: the initial role design is sensible, then a new feature needs a slightly different permission, so someone adds a role. Then a customer needs a variation, so there's another role. After two years you have forty-three roles, half of them differ by one permission from the next, and nobody remembers why the distinction exists.

The pattern that avoids this is separating *roles* from *permissions* explicitly in the data model and keeping the role list small and stable. Roles are named things a person occupies — agent, guest, crew-coordinator, admin — and they map to permission sets. When a new feature needs a new permission, you add the permission and assign it to the existing roles that should have it. You almost never need a new role; you need a new permission. The role list changes at the pace of your org chart; the permission list changes at the pace of your feature roadmap. Conflating them is where the sprawl comes from.

In practice on the AmaWaterways system: we have eight roles, which has held stable for months, and we've added several dozen permissions since I joined. New features declare the permissions they need in a policy attribute; the auth middleware evaluates whether the principal's role set covers those permissions; no new roles were harmed in the making of any of these features. The eight roles will probably still be eight in two years. The permissions will be whatever they need to be, and that's fine.
