---
layout: post
title: "WebGPU for in-browser compute is a real option now"
date: 2025-03-10
author: marcetux
tags: [webgpu, frontend, gpu, browser, javascript]
---
A client wanted to run image preprocessing client-side — resizing and normalizing a
batch of images before upload, to cut server load and reduce round-trips. WebGL was the
old answer and it was always painful to write. WebGPU is the new answer and it's
significantly less painful, which pushed it from "interesting spec" to "thing I
actually shipped" this month.

The mental model for WebGPU is closer to Vulkan than to WebGL — you're working with
pipelines, command encoders, and buffer binding explicitly rather than through an
OpenGL-style global state machine. More setup code upfront, but each piece of setup
corresponds directly to what the GPU is doing, so debugging a wrong result is a matter
of reading the pipeline description rather than guessing which piece of global state got
clobbered. The compute shader is WGSL, which is its own small language but regular
enough that reading a few examples gets you writing functional shaders quickly.

The browser support caveat: Chrome and Edge are solid, Safari shipped it in 2024, Firefox
is still behind a flag as of this writing. For the client's workflow that was fine —
they know their users are Chrome — but for public-facing tools you're still writing a
WebGL fallback. The gap will close; the trajectory is clear. The compute capability is
real enough to plan against now.
