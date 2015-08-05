#!/usr/bin/env bash
# Composite a user face image over an animation frame at specified coordinates.
# Usage: ./face-overlay.sh base_frame.png face.png output.png 120 80 160 160
# Arguments: base face output x y face_w face_h

BASE="$1"
FACE="$2"
OUTPUT="$3"
OVERLAY_X="${4:-0}"
OVERLAY_Y="${5:-0}"
FACE_W="${6:-160}"
FACE_H="${7:-160}"

ffmpeg -loglevel error \
  -i "$BASE" \
  -i "$FACE" \
  -filter_complex \
    "[1:v]scale=${FACE_W}:${FACE_H}[face];
     [0:v][face]overlay=${OVERLAY_X}:${OVERLAY_Y}[out]" \
  -map "[out]" \
  -frames:v 1 \
  "$OUTPUT"
