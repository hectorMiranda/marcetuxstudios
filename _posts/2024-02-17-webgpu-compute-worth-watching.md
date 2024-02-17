---
layout: post
title: "WebGPU compute shaders are worth watching"
date: 2024-02-17
author: marcetux
tags: [webgpu, browser, gpu, performance, frontend]
---
Chrome 113 shipped WebGPU behind a flag last year and it's been stable in more
browsers through early 2024. I spent an afternoon with the compute shader API and
it's not what I expected from a "web graphics" announcement. The interesting part
isn't 3D — it's general-purpose GPU compute in the browser without a plugin or a
native extension.

The programming model is WGSL — WebGPU Shading Language — which is closer to Metal
or modern GLSL than the old WebGL GLSL subset. A compute shader is a function that
runs in parallel across a workgroup grid; you bind input and output buffers, dispatch
the grid, and read results back to CPU. For matrix operations, FFTs, or anything
embarrassingly parallel the performance gap over JavaScript is large. I prototyped a
nearest-neighbor search on embeddings — a baby version of what pgvector does — and
the shader was around 12x faster than the equivalent JS on arrays of the same size.

Where it's headed for AI is obvious: inference on small models in the browser without
a round-trip to a server. There are already early projects running quantized LLMs
this way. The tooling is still rough and the browser compatibility story requires
checking the table every quarter. But this is the kind of API where you want to know
it exists before your PM asks if it's possible. The answer in early 2024 is "yes,
with asterisks, and the asterisks are shrinking."
