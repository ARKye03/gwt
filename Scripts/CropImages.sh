#!/bin/bash

# Directory where the PNG files are located (use current directory by default)
DIR="${1:-.}"

for file in "$DIR"/*.png; do
  if [[ -f "$file" ]]; then
    output="${file%.png}_cropped.png"
    
    ffmpeg -i "$file" -vf "crop=576:1024" "$output"
    echo "Cropped $file -> $output"
  else
    echo "No PNG files found in the directory."
  fi
done
