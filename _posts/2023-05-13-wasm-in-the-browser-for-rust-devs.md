---
layout: post
title: "WASM in the browser, a Rust developer's view"
date: 2023-05-13
author: marcetux
tags: [rust, wasm, frontend, browser]
---
The Casper client has a browser component — wallets need to sign transactions in the
browser — so I finally had a legitimate reason to do what I'd been curious about:
compile the Rust signing code to WASM and run it client-side. The `wasm-bindgen` and
`wasm-pack` toolchain made this less painful than I expected, though the learning
curve has specific edges.

`wasm-bindgen` generates the JavaScript glue that lets your browser JS call Rust
functions as if they were JS functions, handling the type conversion at the boundary.
You annotate Rust functions with `#[wasm_bindgen]`, run `wasm-pack build`, and get an
npm package you can import. The signing code doesn't touch the network and doesn't
need DOM access, which made it the easiest case: pure computation in, bytes out.

The rough edges: the WASM binary size. Debug builds are enormous; release builds
with `opt-level = 'z'` and `wasm-opt` shrink things substantially but you're still
shipping more than a pure JS equivalent would be. The startup cost of initializing the
WASM module is noticeable on the first call — worth caching the init Promise. And the
browser security model means the WASM runs in a sandbox that can't access the
filesystem, which is fine for signing but limits other use cases. For computationally
intensive browser code where correctness is critical, it's a good tool. For everything
else, JS is still cheaper.
