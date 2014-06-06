---
layout: post
title: "Apple announces Swift and what it means for iOS development"
date: 2014-06-05
author: marcetux
tags: [swift, ios, apple, programming-languages]
---
The WWDC keynote this week included a programming language announcement I didn't expect:
Swift, a new language to eventually replace Objective-C for iOS and macOS development.
I'm not primarily an iOS developer but I've written enough Objective-C to have opinions,
and Swift reads like they fixed the parts of Objective-C I've always found genuinely
awkward.

The syntax is dramatically cleaner. No header files, no message-passing brackets, type
inference, closures that look like closures and not the Objective-C block syntax that I
have to look up every time. Optionals as a first-class language feature — the `?` and `!`
operators — bring nullability into the type system instead of leaving it as a runtime
surprise. That alone would have saved me several `EXC_BAD_ACCESS` afternoons. The
playground feature for interactive code evaluation is a genuinely nice idea for learning.

The open question is timeline. Swift 1.0 is shipping with Xcode 6 this fall; production
apps can use it today, but a new language stabilizes over years not months. The standard
library APIs are Swift overlays on Objective-C frameworks, so the documentation lag will
persist. I'm watching it, not jumping. For the new job, iOS isn't the platform
anyway — but understanding what the mobile ecosystem is doing is part of the job.
