#!/usr/bin/env bash
# Build llama.cpp with AVX2 support and run the Llama 2 7B model.
# Requires: git, cmake, gcc, and ~5GB free disk space.

set -euo pipefail

MODELS_DIR="$HOME/models"
MODEL_FILE="llama-2-7b-chat.q4_0.gguf"
LLAMA_DIR="$HOME/llama.cpp"

# Clone and build.
if [ ! -d "$LLAMA_DIR" ]; then
  git clone https://github.com/ggerganov/llama.cpp "$LLAMA_DIR"
fi
cd "$LLAMA_DIR"
git pull
make clean
LLAMA_AVX2=1 make -j"$(nproc)"

# Infer. -ngl 0 = CPU only. -n 256 = max tokens to generate.
# Model must already be downloaded to MODELS_DIR.
./main \
  -m "$MODELS_DIR/$MODEL_FILE" \
  --prompt "You are a helpful assistant. User: Explain Rust lifetimes in two sentences. Assistant:" \
  -n 256 \
  --temp 0.7 \
  --ctx-size 2048 \
  --threads "$(nproc)" \
  --n-gpu-layers 0
