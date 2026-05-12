// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Godot;

namespace Polytoria.Platform;

public sealed class AndroidPlatformService : IPlatformService
{
	public bool IsMobile => OS.GetName() == "Android";
	public bool HasVirtualKeyboard => true;

	public void ShowVirtualKeyboard(string hint = "") => DisplayServer.VirtualKeyboardShow(hint);
	public void HideVirtualKeyboard() => DisplayServer.VirtualKeyboardHide();
	public void SetScreenOrientation(int orientation) => DisplayServer.ScreenSetOrientation((DisplayServer.ScreenOrientation)orientation);
	public void RequestPermission(string permission) => OS.RequestPermission(permission);
	public void Vibrate(int milliseconds) => Input.VibrateHandheld(milliseconds);
	public string GetDeviceModel() => OS.GetModelName();
}
