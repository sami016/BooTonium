using Godot;
using System;

public partial class IconRandomizer : TextureRect
{
	[Export] public Texture2D[] Textures { get; set; }

	public override void _Ready()
	{
		Texture = Textures[new Random().Next(Textures.Length)];
	}

}
