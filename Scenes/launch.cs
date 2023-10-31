using Godot;
using System;

public partial class launch : Node
{
    public override void _Ready()
    {
        SaveManager.Load()
            .ContinueWith(x => CallDeferred("Start"));
    }

    public void Start()
    {
        DisplayServer.WindowSetMode(DisplayServer.WindowMode.ExclusiveFullscreen);
        var levelManager = GetTree().Root
            .GetNode<LevelManager>("./LevelManager");
        levelManager.SetActiveScene("res://Scenes/menu.tscn");
    }
}
