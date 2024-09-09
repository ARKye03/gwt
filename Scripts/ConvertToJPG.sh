#!/bin/bash

if ! command -v ffmpeg &> /dev/null; then
    echo "ffmpeg could not be found"
    exit 1
fi

# Use fd to find all .png files and process them
fd -e png -t f -x /usr/bin/bash -c '
for file; do
    jpg_file="${file%.png}.jpg"
    ffmpeg -i "$file" "$jpg_file" && rm "$file"
done
' bash
