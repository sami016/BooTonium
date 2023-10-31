using Godot;
using System;

public partial class explosion : Node3D
{
	public override void _Ready()
	{
		GetNode<AnimationPlayer>("./AnimationPlayer").AnimationFinished += Finished;
	}

	private void Finished(StringName animName)
	{
		QueueFree();
	}
}
