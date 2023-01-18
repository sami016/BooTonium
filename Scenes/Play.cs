using Godot;
using System;

public partial class Play : Node3D
{
	[Export] public string LevelName { get; set; }

	public override void _Ready()
	{
		GetNode<Label>("./camera/ui/level_name").Text = $"\"{LevelName}\"";
	}

	public override void _Process(double delta)
	{
	}
}
