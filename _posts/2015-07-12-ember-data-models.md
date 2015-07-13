---
layout: post
title: "Ember Data models, first week impressions"
date: 2015-07-12
author: marcetux
tags: [ember, javascript, frontend, spa]
---
First week touching Ember.js in a real codebase and the model layer — Ember Data — is
the first thing I needed to understand. The mental model is different from what I'm used
to: Ember Data has an identity map (one model instance per record type and ID across the
whole app), adapters that handle the API communication, and serializers that handle the
JSON-to-model transformation. The three pieces feel like a lot of ceremony coming from a
simpler REST client pattern, and then you understand the identity map.

The identity map means that if two components show the same user, they're showing the
same JavaScript object — push an attribute update to one and the other updates
automatically without a re-fetch. That's the Ember Data value proposition: consistent,
observable state across the UI for free. In a product with multiple views displaying the
same data — a user's video library showing in a sidebar and a main panel simultaneously
— the alternative is a lot of manual state synchronization.

The adapter configuration is where the "convention over configuration" promise either
pays off or doesn't. JibJab's API mostly follows the JSON:API conventions that Ember
Data assumes, so the adapter works with minimal override. The one nonstandard endpoint
— the render job submission — required a custom action adapter method, which is
documented but not obvious. The convention saves you until it doesn't; when it doesn't,
you need to know where to look.
