#!/usr/bin/env bash
# Benchmark llama.cpp inference speed on Raspberry Pi.
# Run once on Pi 4, once on Pi 5. Compare the tok/s output.

set -euo pipefail

MODEL="${1:-$HOME/models/llama-2-7b-chat.q4_0.gguf}"
THREADS="${2:-4}"  # Pi 4: 4 cores; Pi 5: 4 cores

# Standard prompt for repeatable measurement.
PROMPT="Explain in detail how the Rust borrow checker prevents data races. Be thorough."

./main \
  -m "$MODEL" \
  --prompt "$PROMPT" \
  -n 200 \
  --threads "$THREADS" \
  --n-gpu-layers 0 \
  --temp 0 \
  2>&1 | grep -E "(llama_print_timings|eval time|tok/s)"

# Expected output lines:
# llama_print_timings: eval time = XXXXX ms / YYY tokens (  Z ms per token, W tokens per second)
