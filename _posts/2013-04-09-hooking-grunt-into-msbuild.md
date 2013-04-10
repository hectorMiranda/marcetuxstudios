---
layout: post
title: "Hooking Grunt into the MSBuild pipeline"
date: 2013-04-09
author: marcetux
tags: [dotnet, grunt, build, msbuild, devops]
---
For months I've had two build processes that don't know about each other: MSBuild does
the C# compilation, and Grunt does the JavaScript and CSS. The friction is the CI
server, which runs MSBuild and therefore doesn't run Grunt, which means the published
site is missing the concatenated, minified assets — the ones that only exist after Grunt
runs.

The fix is a `BeforeBuild` target in the `.csproj` file that shells out to `grunt` before
MSBuild starts. The project file is XML and MSBuild understands `<Exec Command="..." />`
perfectly well. Add node.js and the `grunt-cli` to the CI agent, run `npm install` in
the same pre-build target, and MSBuild is now the entry point for the entire build:
C# compilation *and* the front-end pipeline. One command, one result, on the dev machine
and the CI server both.

The small gotcha is path handling — `grunt` needs to resolve from `node_modules/.bin`
and the working directory has to be the project root, not the MSBuild working directory.
A `WorkingDirectory` attribute on the `Exec` element sorts it. The discipline this
enforces is good: if the Grunt build breaks, the MSBuild breaks, which means a broken
front-end build can no longer silently ship to production while the C# tests all pass.
The pain is appropriately visible.
