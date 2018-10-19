---
layout: post
title: "Angular 7 and what changed for a large enterprise app"
date: 2018-10-19
author: marcetux
tags: [angular, typescript, frontend, javascript, enterprise]
---
Angular 7 shipped this week and the upgrade on the CTM portal project took an hour
— the automated schematics handled the import changes, and the only manual step was
the `ng-template` attribute API change that affected two of our custom components.
The upgrade cadence that Angular has maintained since version 4 is one of the things
a large enterprise can actually plan around, which matters more than the headline
features in any individual release.

The headline feature this cycle is the virtual scrolling module in the CDK. For the
data tables in the bank portal — account transaction lists that can be thousands of
rows — virtual scrolling means rendering only the visible viewport rather than the
full DOM. We had implemented this manually with a third-party library; `CdkVirtualScrollViewport`
replaces it with something the framework team will maintain. The drag-and-drop CDK
module is similar: a well-designed, accessible implementation that replaces several
third-party dependencies with a maintained one.

The pattern I notice after several Angular major versions: the CDK absorbs the
categories of problems that used to require third-party libraries, one release at a
time. Overlay, drag-and-drop, virtual scroll, table. For a long-lived application
this is valuable — a maintained first-party implementation ages better than a
dependency that may or may not follow the Angular release train.
