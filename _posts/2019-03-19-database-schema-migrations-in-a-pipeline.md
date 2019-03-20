---
layout: post
title: "Database schema migrations in a CI/CD pipeline"
date: 2019-03-19
author: marcetux
tags: [databases, devops, ci, migrations, architecture]
---
The last untreated gap in our delivery pipeline was database schema changes. Application code deployed via YAML pipeline, clean and reviewed. Schema changes: a DBA running scripts in a SQL Server Management Studio window, manually, with a logged ticket. The ticket was the audit trail. I've been working on replacing the window with a pipeline.

The tool we landed on is DbUp, which is minimal and blunt: a collection of SQL migration scripts with version numbers in the filename, applied in order, with a journal table recording what's been run. It's not fancy — no rollback support, no declarative diff — but it's understandable, which matters a lot in a regulated environment. The pipeline step builds the app, applies migrations against the target environment in a pre-deploy step, then deploys the application code. If the migration fails, the deployment stops and the previous application version stays running against the previous schema.

The constraint this imposes is backward-compatible migrations. You can never drop a column the current application version is reading. The discipline is: first deploy adds the new column as nullable, second deploy migrates data and makes it non-null, third deploy removes the old column after the application no longer references it. More migrations, more steps, but each step is independently safe to deploy. The DBA is now reviewing SQL files in a pull request rather than running them by hand. The audit trail is the pipeline run. Both of those feel like improvements.
