---
layout: post
title: "Containerizing .NET services the right way"
date: 2019-08-14
author: marcetux
tags: [docker, dotnet, containers, devops, kubernetes]
---
Three things I keep seeing wrong in .NET Docker images that teams generate from the default Visual Studio template and then never revisit: running as root, including the SDK in the final image, and copying everything from the build context instead of using multi-stage builds. Each is a fixable mistake with a meaningful impact.

The multi-stage build separates the SDK stage (for building) from the runtime stage (what ships). You use `mcr.microsoft.com/dotnet/sdk:3.0` to publish the application, then `FROM mcr.microsoft.com/dotnet/aspnet:3.0` for the final image and copy only the published output. The final image doesn't have the compiler, the NuGet cache, or the source code — it has the runtime and the binary. Image size drops by several hundred megabytes. Attack surface drops proportionally.

Running as a non-root user is two lines: `RUN adduser --disabled-password --gecos '' appuser` and then `USER appuser` before the entrypoint. Kubernetes will enforce this through a security context if you configure one, but setting it in the Dockerfile means it's right regardless of how the container is run. The security context `runAsNonRoot: true` in the pod spec becomes a second layer that validates the Dockerfile did the right thing. Containers aren't magic security boundaries, but not running as root closes the most obvious path from container escape to host impact. Worth two lines every time.
