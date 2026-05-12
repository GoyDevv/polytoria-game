// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Godot;

namespace Polytoria.Platform;

public sealed class DesktopPlatformService : IPlatformService
{
	public bool IsMobile => false;
	public bool HasVirtualKeyboard => false;

	public void ShowVirtualKeyboard(string hint = "") { }
	public void HideVirtualKeyboard() { }
	public void SetScreenOrientation(int orientation) { }
	public void RequestPermission(string permission) { }
	public void Vibrate(int milliseconds) { }
	public string GetDeviceModel() => OS.GetModelName();
}
