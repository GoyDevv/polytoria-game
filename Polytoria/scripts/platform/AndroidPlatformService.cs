// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Godot;

namespace Polytoria.Platform;

public class AndroidPlatformService : IPlatformService
{
    public bool IsMobile => true;
    public bool HasVirtualKeyboard => DisplayServer.HasFeature(DisplayServer.Feature.VirtualKeyboard);

    public void ShowVirtualKeyboard(string hint = "")
    {
        if (HasVirtualKeyboard)
            DisplayServer.VirtualKeyboardShow(hint);
    }

    public void HideVirtualKeyboard()
    {
        if (HasVirtualKeyboard)
            DisplayServer.VirtualKeyboardHide();
    }

    public void SetScreenOrientation(DisplayServer.ScreenOrientation orientation)
    {
        DisplayServer.ScreenSetOrientation(orientation);
    }

    public void RequestPermission(string permission)
    {
        OS.RequestPermission(permission);
    }

    public void Vibrate(int milliseconds)
    {
        Input.VibrateHandheld(milliseconds);
    }

    public string GetDeviceModel()
    {
        return OS.GetModelName();
    }
}
