# This Source Code Form is subject to the terms of the Mozilla Public
# License, v. 2.0. If a copy of the MPL was not distributed with this
# file, You can obtain one at https://mozilla.org/MPL/2.0/.

extends Node

var service = null

func _ready() -> void:
	if OS.get_name() == "Android":
		service = load("res://scripts/platform/AndroidPlatformService.cs").new()
	else:
		service = load("res://scripts/platform/DesktopPlatformService.cs").new()
