using Godot;
using System;

public partial class LevelManager : Node
{
    private PackedScene _activeLevel;

    public void SetActiveScene(string scene)
    {
        var sceneResource = ResourceLoader.Load<PackedScene>(scene);
        SetActiveScene(sceneResource);
    }

    public void SetActiveScene(PackedScene scene)
    {
        var root = GetTree().Root;
        if (root.HasNode("Level"))
        {
            var existingLevel = GetTree().Root.GetNode<Node>("Level");
            GetTree().Root.RemoveChild(existingLevel);
        }
        _activeLevel = scene;
        var newLevel = scene.Instantiate();
        newLevel.Name = "Level";
        GetTree().Root.AddChild(newLevel);
    }

    public void RestartLevel()
    {
        if (_activeLevel == null)
        {
            return;
        }

        SetActiveScene(_activeLevel);
    }
}
