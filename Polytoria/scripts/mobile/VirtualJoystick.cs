// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Godot;

namespace Polytoria.Client.UI.Mobile;

public partial class VirtualJoystick : Control
{
	[Export] public float DeadZone = 0.15f;
	[Export] public float MaxRadius = 80f; // Will be scaled by DPI
	[Export] public string ActionLeft = "leftward";
	[Export] public string ActionRight = "rightward";
	[Export] public string ActionForward = "forward";
	[Export] public string ActionBackward = "backward";

	private Vector2 _startPos;
	private Vector2 _currentPos;
	private bool _isDragging = false;
	private float _radiusScale = 1.0f;
	private int _touchIndex = -1;

	private Control _knob = null!;

	public override void _Ready()
	{
		_radiusScale = DisplayServer.ScreenGetDpi() / 160f; // Assuming 160 DPI is baseline 1.0 scale
		if (_radiusScale <= 0) _radiusScale = 1.0f;

		_knob = new Control();
		AddChild(_knob);
		// Simple knob rendering, can be upgraded with TextureRects later
	}

	public override void _GuiInput(InputEvent @event)
	{
		if (@event is InputEventScreenTouch touch)
		{
			if (touch.Pressed && !_isDragging)
			{
				_isDragging = true;
				_touchIndex = touch.Index;
				_startPos = touch.Position;
				_currentPos = touch.Position;
				AcceptEvent();
			}
			else if (!touch.Pressed && touch.Index == _touchIndex)
			{
				_isDragging = false;
				_touchIndex = -1;
				ResetInput();
				AcceptEvent();
			}
		}
		else if (@event is InputEventScreenDrag drag && drag.Index == _touchIndex)
		{
			_currentPos = drag.Position;
			UpdateInput();
			AcceptEvent();
		}
	}

	private void UpdateInput()
	{
		Vector2 delta = _currentPos - _startPos;
		float length = delta.Length();
		float maxRad = MaxRadius * _radiusScale;

		if (length > maxRad)
		{
			delta = delta.Normalized() * maxRad;
			_startPos = _currentPos - delta; // Move the base to follow the finger if pulled too far
		}

		Vector2 normalized = delta / maxRad;

		if (normalized.Length() < DeadZone)
		{
			ResetInput();
			return;
		}

		// Inject inputs
		InjectAction(ActionLeft, -normalized.X);
		InjectAction(ActionRight, normalized.X);
		InjectAction(ActionForward, -normalized.Y);
		InjectAction(ActionBackward, normalized.Y);
	}

	private void ResetInput()
	{
		InjectAction(ActionLeft, 0);
		InjectAction(ActionRight, 0);
		InjectAction(ActionForward, 0);
		InjectAction(ActionBackward, 0);
	}

	private void InjectAction(string action, float strength)
	{
		float val = Mathf.Clamp(strength, 0, 1);

		InputEventJoypadMotion motion = new()
		{
			Axis = action switch
			{
				"leftward" or "rightward" => JoyAxis.LeftX,
				"forward" or "backward" => JoyAxis.LeftY,
				_ => JoyAxis.LeftX
			},
			AxisValue = strength * (action == "leftward" || action == "forward" ? -1 : 1)
		};
		motion.SetMeta("emulated", 1);
		Input.ParseInputEvent(motion);
	}
}
