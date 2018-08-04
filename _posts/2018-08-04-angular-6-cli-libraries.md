---
layout: post
title: "Angular 6 CLI and the library build story"
date: 2018-08-04
author: marcetux
tags: [angular, typescript, frontend, javascript, tooling]
---
Angular 6 has been out since May and I finally gave it the proper upgrade treatment
on the CTM portal project. The `ng update` command is the thing I wasn't expecting
to work as well as it did — it reads the upgrade guide, applies schematics to your
source files, and handles the mechanical migration automatically. I've done enough
Angular upgrades by hand to appreciate the difference between "read the changelog and
grep for deprecated imports" and "run one command and review the diff."

The library support in CLI 6 is the part worth discussing. The `ng generate library`
workflow produces an npm-publishable package from within an Angular workspace, with
a proper entry point, a generated `public-api.ts`, and a separate tsconfig that treats
the library boundary correctly. We extracted the CTM booking-flow components into a
library shared between the customer portal and the internal admin app, which is the
use case this was clearly designed for.

The secondary entry points pattern — exporting multiple paths from one package — is
where the Angular Package Format gets opinionated about your directory structure. The
constraints are right (it forces you to think about the public API surface), but the
documentation is thin enough that I spent an afternoon figuring out why my secondary
entry point wasn't resolving in the consuming app. The answer was the tsconfig path
mapping, which the CLI should probably handle but doesn't yet.
