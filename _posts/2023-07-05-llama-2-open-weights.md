---
layout: post
title: "Llama 2, open weights and what that changes"
date: 2023-07-05
author: marcetux
tags: [llm, llama, open-source, ai]
---
Meta released Llama 2 yesterday with a license that allows commercial use, which is
a different proposition from the leaked original weights. A capable language model
with a real license changes what you can build and deploy without worrying about a
terms-of-service conversation at 2 AM when your project is about to ship.

The model comes in three sizes: 7B, 13B, and 70B parameters. The 7B fits in about
4GB of RAM quantized to 4-bit; a modern MacBook with 16GB can run it without a GPU.
The 13B needs about 8GB quantized; the 70B needs serious hardware. The Llama 2 Chat
variants are fine-tuned for conversation and follow instructions noticeably better
than the base models. For most use cases where you'd reach for GPT-3.5-turbo, the 13B
chat model is now a viable local alternative — not identical quality, but close enough
for a lot of tasks.

The thing that changes structurally: RAG over private data now has a fully local path.
Embed locally with `sentence-transformers`, store in pgvector on a machine you own,
generate with a Llama 2 model running locally. No data leaves your network. That's
not compelling for a startup building a product, but it's very compelling for anyone
with data sensitivity requirements or a policy against third-party API calls in
production. The local option went from "hobbyist curiosity" to "credible architecture"
with this release.
