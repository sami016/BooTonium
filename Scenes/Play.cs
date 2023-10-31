using Godot;
using System;

public enum ObjectiveType
{
    Water,
    Turbines
}

public partial class Play : Node3D
{
    [Export] public string LevelName { get; set; }

    [Export] public ObjectiveType ObjectiveType { get; set; }
    
    public override void _Ready()
    {
        GetNode<Label>("./camera/ui/level_name").Text = $"\"{LevelName}\"";
        GetNode<Label>("./camera/ui/objective_text").Text = $"\"{GetObjectiveText()}\"";
    }

    private string GetObjectiveText()
    {
        switch (ObjectiveType)
        {
            case ObjectiveType.Water:
                return "React ghosts with holy water to complete the level.";
            case ObjectiveType.Turbines:
                return "Get all turbines spinning to complete the level.";
        }
        return "";
    }

	public override void _Process(double delta)
	{
	}
}
