// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Godot;

namespace Polytoria.Mobile;

public partial class MobileQualityProfile : Node
{
	public override void _Ready()
	{
		if (OS.GetName() == "Android" || OS.GetName() == "iOS")
		{
			Engine.PhysicsTicksPerSecond = 30;

			// Global mobile LOD settings
			GetViewport().Scaling3DMode = Viewport.Scaling3DModeEnum.Bilinear;
			GetViewport().Scaling3DScale = 0.8f; // Render at 80% resolution for performance
		}
	}
}
