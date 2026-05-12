#!/bin/bash
wget -q https://github.com/godotengine/godot/releases/download/4.2.2-stable/Godot_v4.2.2-stable_mono_linux_x86_64.zip
unzip -q Godot_v4.2.2-stable_mono_linux_x86_64.zip
chmod +x Godot_v4.2.2-stable_mono_linux_x86_64/Godot_v4.2.2-stable_mono_linux.x86_64
cd Polytoria
../Godot_v4.2.2-stable_mono_linux_x86_64/Godot_v4.2.2-stable_mono_linux.x86_64 --headless --export-release "Android" ../build/polytoria.apk > ../godot_log.txt 2>&1
