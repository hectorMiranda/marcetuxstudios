---
layout: post
title: "Unity 2D tilemaps: what I learned in 48 hours"
date: 2022-07-16
author: marcetux
tags: [unity, gamedev, 2d, tilemap]
---
The game jam forced me to learn Unity 2D tilemaps at hackathon speed, which is the
worst possible learning condition and also extremely effective. The tilemap system in
modern Unity (2020.3 LTS, what we used) is genuinely good: you define a palette of
tiles from a sprite sheet, paint them onto a Tilemap component, and the engine handles
batching and rendering. The Grid parent component handles the coordinate system, so
your code works in tile coordinates and Unity handles the world space translation.

The physics integration is what surprised me most. You can add a TilemapCollider2D
component to a tilemap and Unity generates composite colliders automatically for the
painted tiles — no hand-drawing collision shapes. Add a RuleTile and you can define
auto-tiling rules so transitions between tile types (ground to dirt, wall to open air)
pick the right sprite automatically. The artist on our team started painting the level
and it looked right without any additional code. That's a well-designed tool.

What I would do differently with more time: separate the visual layer from the logic
layer using two tilemaps stacked — one for rendering, one for collision logic — so you
can update the visual tiles independently. We did everything on one tilemap and painted
ourselves into a corner when we needed invisible collision regions the player couldn't
see. Same lesson as every rendering architecture: the visual representation and the
logical state should not be the same data structure.
