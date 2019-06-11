---
layout: post
title: "Integration testing with real dependencies using Testcontainers"
date: 2019-06-10
author: marcetux
tags: [testing, dotnet, docker, integration, devops]
---
Unit tests with mocked dependencies tell you your code is internally consistent. They don't tell you whether your SQL queries work against a real database schema, whether your Service Bus message format survives the actual serializer, or whether your migrations ran in the order you think they did. We had a category of bug that only appeared in the staging environment — always the same category, always a gap between the mock and the real system.

Testcontainers for .NET spins up a Docker container for each external dependency as part of the test run. The SQL Server test runs against a real SQL Server container. The Redis test runs against a real Redis container. The container starts before the tests, the test fixture migrates the schema using DbUp (so migration order gets tested too), the tests run, the container stops. The CI pipeline has Docker; the developer's machine has Docker; it works the same place.

The pattern we've settled on is an `IntegrationTestBase` fixture that handles the container lifecycle and exposes connection strings to the test methods. Slow tests — a SQL Server container adds thirty seconds to spin up — so we run integration tests in a separate pipeline stage from unit tests and don't block the PR merge gate on them. They run on the main branch after merge and on release branches. The rule: if it's integration-worthy to test, it's important enough to run on a real dependency, and it's acceptable for that to be slower. Mock fidelity is a tax on your future self's debugging time.
