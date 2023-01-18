using Godot;
using System;

public partial class ItemSelect : VBoxContainer
{
	[Export] public PlayerBuildPlacement PlayerBuildPlacement { get; set; }

	public override void _Ready()
	{
		PlayerBuildPlacement.TypeChanged += TypeChanged;
		TypeChanged();
    }

	private void TypeChanged()
    {
		Reset();

		var child = GetNode($"./{(int)PlayerBuildPlacement.Type}");
        child.GetNode<TextureRect>("./selected").Visible = true;
    }

	private void Reset()
	{
		foreach (var child in GetChildren())
		{
			child.GetNode<TextureRect>("./selected").Visible = false;
		}
	}
}
