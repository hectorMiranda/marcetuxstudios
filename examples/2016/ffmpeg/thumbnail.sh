#!/bin/bash
# Extract thumbnails at three common sizes from a video file.
# Usage: ./thumbnail.sh input.mp4 output_prefix
# Output: output_prefix_320.jpg output_prefix_640.jpg output_prefix_1280.jpg

INPUT="$1"
PREFIX="$2"

if [[ -z "$INPUT" || -z "$PREFIX" ]]; then
  echo "Usage: $0 input.mp4 output_prefix" >&2
  exit 1
fi

# Seek to 10% of duration; -vframes 1 extracts a single frame
DURATION=$(ffprobe -v error -show_entries format=duration \
  -of default=noprint_wrappers=1:nokey=1 "$INPUT")
SEEK=$(echo "$DURATION * 0.10" | bc)

for WIDTH in 320 640 1280; do
  ffmpeg -y -ss "$SEEK" -i "$INPUT" \
    -vframes 1 \
    -vf "scale=${WIDTH}:-2" \
    -q:v 4 \
    "${PREFIX}_${WIDTH}.jpg"
done

echo "Thumbnails written: ${PREFIX}_320.jpg ${PREFIX}_640.jpg ${PREFIX}_1280.jpg"
