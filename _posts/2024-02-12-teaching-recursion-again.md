---
layout: post
title: "Teaching recursion again and what I learned from it"
date: 2024-02-12
author: marcetux
tags: [cs, teaching, algorithms, tutoring]
---
I've explained recursion a dozen times over the years, half of them to myself. The
explanation that finally stuck for a tutoring student last week surprised me: I
stopped using factorial and started using a drawer full of drawers. You're trying to
find your keys. You open a drawer. It contains either keys or more drawers. You call
the same process on each sub-drawer. The base case is a drawer with only keys.

The abstract structure clicked once it was a physical thing they could picture
instead of a mathematical definition they had to believe. Then we translated it to
code — Python, because that's what they're learning — and the recursion looked like
what it already was in their head. I've noticed that the order matters: concrete
story first, code second, mathematical proof if they want it third. The textbook
goes the other direction and most students get lost at step one.

The part I learned from them: they asked what happens when the drawers are circular
— a drawer that contains itself. We talked through the call stack, then the stack
overflow, then why the base case is mandatory and not optional decoration. I'd been
presenting base cases as "this is how you write it correctly" for years. The
circular-drawer question made me see that the base case is the *only* thing that
makes the problem finite. Sometimes a student's question opens the room a little
wider.
