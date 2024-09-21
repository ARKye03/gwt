#!/bin/bash

# Directorio donde Doxygen generó los archivos HTML
html_dir="./Docs/html"
md_dir="./Docs/markdown"

# Crear el directorio para los archivos Markdown si no existe
mkdir -p "$md_dir"

# Convertir cada archivo HTML a Markdown
for html_file in "$html_dir"/*.html; do
  # Obtener el nombre base del archivo (sin la extensión .html)
  base_name=$(basename "$html_file" .html)
  
  # Convertir el archivo HTML a Markdown
  pandoc -f html -t markdown -o "$md_dir/$base_name.md" "$html_file"
done

# Eliminar los archivos HTML
rm -r "$html_dir"
