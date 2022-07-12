---
layout: post
title: "Dogeathon in Sydney: a game jam with Unity 2D and a lot of instant coffee"
date: 2022-07-11
author: marcetux
tags: [gamedev, unity, hackathon, travel, web3]
---
I flew to Sydney in early July for the Dogeathon — a Casper-adjacent hackathon with a
game-jam track, themed loosely around the Casper ecosystem. Forty-eight hours in a
converted warehouse near Surry Hills, three people to a team, and the brief was "build
something interesting that uses the Casper network." Our team picked a Unity 2D idle
game where in-game resources are represented as on-chain tokens. I hadn't opened Unity
seriously in years; my teammates hadn't touched game dev at all. We shipped something
that ran, which is the first miracle of any game jam.

The Unity 2D work was unexpectedly fun. The tilemap editor is much improved from the
last time I used it, and the 2D physics system lets you throw together a convincing-
looking scene in an afternoon. We built a side-scrolling resource collector — the
aesthetic was "retro pixel art space mine" — and wired up Casper token transfers to
in-game purchase events through a WebGL bridge between the browser wallet and the Unity
runtime. The integration was the hardest thirty percent of the work and the part we
almost didn't finish.

We came second in our track, which in a forty-eight-hour jam with no sleep means we
built something that actually worked and someone else built something slightly more
impressive. I'll take it. The part that stays with me isn't the Unity code or the
smart contract — it's watching a team of strangers build something real in two days by
picking up tools they'd never used before and refusing to stop. Game jams are the most
honest version of "just ship it," and Sydney in July is a perfectly good place to
do it.
