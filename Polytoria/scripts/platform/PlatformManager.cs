// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Godot;

namespace Polytoria.Platform;

public partial class PlatformManager : Node
{
    public static IPlatformService Service { get; private set; } = null!;

    public override void _Ready()
    {
        if (OS.GetName() == "Android")
        {
            Service = new AndroidPlatformService();
        }
        else
        {
            Service = new DesktopPlatformService();
        }
    }
}
