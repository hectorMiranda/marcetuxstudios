#!/bin/bash
# Package a video into three HLS renditions plus a master playlist.
# Usage: ./hls-package.sh input.mp4 output_dir
# Output: output_dir/master.m3u8, 360p/, 540p/, 720p/ subdirs with segments

INPUT="$1"
OUTDIR="$2"

if [[ -z "$INPUT" || -z "$OUTDIR" ]]; then
  echo "Usage: $0 input.mp4 output_dir" >&2
  exit 1
fi

mkdir -p "${OUTDIR}/360p" "${OUTDIR}/540p" "${OUTDIR}/720p"

# 360p rendition
ffmpeg -y -i "$INPUT" \
  -vf scale=640:360 -b:v 500k -maxrate 550k -bufsize 1000k \
  -c:v libx264 -profile:v main -level 3.1 \
  -c:a aac -b:a 96k \
  -hls_time 4 -hls_playlist_type vod \
  -hls_segment_filename "${OUTDIR}/360p/seg%03d.ts" \
  "${OUTDIR}/360p/index.m3u8"

# 540p rendition
ffmpeg -y -i "$INPUT" \
  -vf scale=960:540 -b:v 1000k -maxrate 1100k -bufsize 2000k \
  -c:v libx264 -profile:v main -level 3.1 \
  -c:a aac -b:a 128k \
  -hls_time 4 -hls_playlist_type vod \
  -hls_segment_filename "${OUTDIR}/540p/seg%03d.ts" \
  "${OUTDIR}/540p/index.m3u8"

# 720p rendition
ffmpeg -y -i "$INPUT" \
  -vf scale=1280:720 -b:v 2000k -maxrate 2200k -bufsize 4000k \
  -c:v libx264 -profile:v high -level 4.0 \
  -c:a aac -b:a 128k \
  -hls_time 4 -hls_playlist_type vod \
  -hls_segment_filename "${OUTDIR}/720p/seg%03d.ts" \
  "${OUTDIR}/720p/index.m3u8"

# Write master playlist
cat > "${OUTDIR}/master.m3u8" <<'M3U'
#EXTM3U
#EXT-X-VERSION:3
#EXT-X-STREAM-INF:BANDWIDTH=500000,RESOLUTION=640x360
360p/index.m3u8
#EXT-X-STREAM-INF:BANDWIDTH=1000000,RESOLUTION=960x540
540p/index.m3u8
#EXT-X-STREAM-INF:BANDWIDTH=2000000,RESOLUTION=1280x720
720p/index.m3u8
M3U

echo "HLS package written to ${OUTDIR}/master.m3u8"
