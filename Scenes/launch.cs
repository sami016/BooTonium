using Godot;
using System;

public partial class launch : Node
{
    public override void _Ready()
    {
        CallDeferred("Start");
    }

    public void Start()
    {
        var levelManager = GetTree().Root
            .GetNode<LevelManager>("./LevelManager");
        levelManager.SetActiveScene("res://Scenes/menu.tscn");
    }
}
