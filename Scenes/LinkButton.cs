using Godot;
using System;

public partial class LinkButton : Button
{
	[Export] public string LoadScene { get; set; }

    private LevelManager _levelManager;

    public override void _Ready()
	{
		Pressed += OnPressed;
        _levelManager = GetTree().Root.GetNode<LevelManager>("LevelManager");
    }

    private void OnPressed()
	{
        _levelManager.SetActiveScene(LoadScene);
	}
}
