---
layout: post
title: "RBAC for the seller portal"
date: 2017-03-19
author: marcetux
tags: [dotnet, security, architecture, api]
---
The seller portal at SolidCommerce has outgrown the "is this user an admin" boolean.
We now have sellers, seller employees, channel managers, and internal support staff —
each of whom should see a different slice of the data and have different write
permissions. Bolting more booleans onto the user record is where that road leads, and
I've been down it before.

Role-Based Access Control is the boring correct answer. A user has one or more roles;
a role has a set of permissions; a permission is a resource-action pair like
`inventory:write` or `channel:read`. The middleware checks the permission, not the role
directly — so when we add a new permission, we add it to the roles that need it, not to
every access check in the application. The check sites are stable; the role definitions
are the thing that evolves.

The implementation in .NET is Claims-based: roles and permissions are claims on the
identity, and the authorization middleware checks claims rather than a custom "is user
allowed to do X" method scattered through controllers. The policy syntax in ASP.NET Core
makes this clean — define a policy per permission string, apply `[Authorize(Policy =
"inventory:write")]` at the controller or action level, and the framework handles the
check. The alternative — authorization logic duplicated in every controller — is how you
end up with a permission check that's wrong in exactly the endpoint the attacker tries.
