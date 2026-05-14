// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Godot;

namespace Polytoria.Platform;

public interface IPlatformService
{
    bool IsMobile { get; }
    bool HasVirtualKeyboard { get; }
    void ShowVirtualKeyboard(string hint = "");
    void HideVirtualKeyboard();
    void SetScreenOrientation(Godot.DisplayServer.ScreenOrientation orientation);
    void RequestPermission(string permission);
    void Vibrate(int milliseconds);
    string GetDeviceModel();
}
