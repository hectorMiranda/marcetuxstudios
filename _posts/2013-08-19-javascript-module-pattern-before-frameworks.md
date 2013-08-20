---
layout: post
title: "The JavaScript module pattern and why it still matters under Angular"
date: 2013-08-19
author: marcetux
tags: [javascript, architecture, frontend, tooling]
---
New developer on the front end wrote some utility code as a plain function dumped at
the top of a script file — works, but it's a global. Adding another script that defines
a variable with the same name silently shadows it. This is the JavaScript global
pollution problem that module systems solve, and it's worth understanding the solution
before the framework handles it.

The immediately-invoked function expression (IIFE) is the classic answer: wrap the code
in `(function() { ... })()` and nothing inside leaks to the global scope. Expose only
what callers need by returning an object: `var MyModule = (function() { function
privateHelper() {...} function publicMethod() { privateHelper(); } return { publicMethod:
publicMethod }; })()`. Only `MyModule.publicMethod` is global; `privateHelper` doesn't
exist outside the IIFE. The pattern composes — each file wraps its own IIFE, and the
returned objects form a namespace.

Angular's own module system (`angular.module('dash')`) does this better for Angular
code — the DI container means you almost never define a global — but utility code that
doesn't belong in Angular's IoC still benefits from the IIFE pattern. The discipline of
asking "does this need to be global?" before adding a function is worth building
independent of whatever framework you're using. Global state is technical debt that
compounds silently until it breaks noisily.
