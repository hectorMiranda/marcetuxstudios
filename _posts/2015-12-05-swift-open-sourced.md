---
layout: post
title: "Swift goes open source"
date: 2015-12-05
author: marcetux
tags: [swift, ios, apple, open-source]
---
Apple open-sourced Swift on December 3rd with the full commit history on GitHub, a Linux
port, and a public evolution process. I've been watching Swift since the announcement in
June 2014 but hadn't invested deeply because "Apple's new proprietary language for Apple
platforms" didn't feel like the place to put time if you care about portability. The open
source release changes that calculus.

The language itself has moved quickly since 2014. Swift 2.0, released in September,
added error handling with `do/try/catch` and protocol extensions — the ability to add
default implementations to protocols, which changes the composition story significantly.
The Linux port means Swift is no longer exclusively a Xcode-and-iOS story; you can
write server-side Swift if you want to, and a few companies are already doing so. The
package manager that ships with the open-source release is basic but functional.

I'm not rewriting JibJab's Ruby API in Swift. The practical use case for me in the near
term is iOS app development — the personalized video sharing flow is something the mobile
team has been planning in Objective-C and Swift is the cleaner option for new work.
What the open-source release signals is that Apple is committed to Swift as the platform
language long-term, which makes the investment decision easier. Objective-C isn't going
anywhere; Swift is where new iOS code should start.
