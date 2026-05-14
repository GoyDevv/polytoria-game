#!/bin/bash
set -e
GODOT=/path/to/godot4_mono
PROJECT=./Polytoria/project.godot
OUT=./build/polytoria.apk

echo "Building .NET assembly..."
dotnet build Polytoria.sln -c Release

echo "Exporting Android APK..."
$GODOT --headless --export-release "Android" $OUT --path ./Polytoria

echo "Done: $OUT"
