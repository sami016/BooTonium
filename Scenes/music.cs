using Godot;
using System;

public partial class music : Node
{
	private AudioStreamPlayer _player;

	public override void _Ready()
	{
		_player = GetNode<AudioStreamPlayer>("./AudioStreamPlayer");
		_player.Play();
    }

	public override void _Process(double delta)
	{
	}
}
