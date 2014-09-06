---
layout: post
title: "Rack middleware and what Rails is built on top of"
date: 2014-09-06
author: marcetux
tags: [ruby, rack, rails, middleware, web]
---
A request logging requirement came in — every API request should record the IP, user
agent, and response time — and the Rails filter approach was going to touch every
controller. Someone on the team mentioned Rack middleware and after an hour I understood
why it was the right answer.

Rack is the web server interface that Ruby frameworks sit on top of. A Rack application
is any object that responds to `call` with an environment hash and returns a response
triplet. Middleware is a Rack app that wraps another Rack app: it receives the request,
optionally does something before passing it to the next layer, then receives the
response and optionally does something before returning it. The stack of middleware is
exactly the pipeline model from OWIN, which I understood from the Edgecast work.

Writing the logging middleware was about fifteen lines. `def call(env)` starts the clock,
passes to `@app.call(env)`, and records the response in the `ensure` block. `config/
application.rb` gets one `config.middleware.insert_before :ActionDispatch::Static,
RequestLogger` line, and every request goes through it — no controller changes required.
Middleware is where cross-cutting concerns belong in a Rack stack, for the same reason
they belong in OWIN for .NET and servlet filters for Java. The pattern is the pattern.
