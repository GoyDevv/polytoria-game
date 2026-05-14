// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Godot;

namespace Polytoria.Client.UI.Mobile;

public partial class TouchCameraController : Control
{
	[Export] public float Sensitivity = 0.5f;
	[Export] public float ZoomSensitivity = 0.1f;

	private bool _isDragging = false;
	private int _touchIndex = -1;

	public override void _GuiInput(InputEvent @event)
	{
		if (@event is InputEventScreenTouch touch)
		{
			if (touch.Pressed && !_isDragging)
			{
				_isDragging = true;
				_touchIndex = touch.Index;
				AcceptEvent();
			}
			else if (!touch.Pressed && touch.Index == _touchIndex)
			{
				_isDragging = false;
				_touchIndex = -1;
				AcceptEvent();
			}
		}
		else if (@event is InputEventScreenDrag drag && drag.Index == _touchIndex)
		{
			InputEventMouseMotion motion = new()
			{
				Relative = drag.Relative * Sensitivity,
				Position = drag.Position,
				ButtonMask = MouseButtonMask.Right // Emulate right click drag for camera
			};
			motion.SetMeta("emulated", 1);
			Input.ParseInputEvent(motion);
			AcceptEvent();
		}
		else if (@event is InputEventMagnifyGesture mag)
		{
			// Emulate scroll for zoom
			InputEventMouseButton scroll = new()
			{
				ButtonIndex = mag.Factor < 1.0f ? MouseButton.WheelDown : MouseButton.WheelUp,
				Pressed = true
			};
			Input.ParseInputEvent(scroll);
			AcceptEvent();
		}
	}
}
