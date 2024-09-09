#!/bin/bash

if ! command -v ffmpeg &> /dev/null; then
    echo "ffmpeg could not be found"
    exit 1
fi

for file in *.jpg *.jpeg; do
    if [ -e "$file" ]; then
        output_file="${file%.*}.png"

        echo "Converting $file to $output_file"
        ffmpeg -i "$file" "$output_file"
    fi
done

