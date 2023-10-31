using Godot;
using System;

public partial class LinkButton : Button
{
	[Export] public string LoadScene { get; set; }
    [Export] public int LevelIndex { get; set; } = -1;

    private LevelManager _levelManager;

    public override void _Ready()
	{
        if (LevelIndex != -1)
        {
            if (SaveManager.SaveData.Position < LevelIndex)
            {
                Disabled = true;
                return;
            }
        }

		Pressed += OnPressed;
        _levelManager = GetTree().Root.GetNode<LevelManager>("LevelManager");
    }

    private void OnPressed()
	{
        _levelManager.SetActiveScene(LoadScene);
	}
}
