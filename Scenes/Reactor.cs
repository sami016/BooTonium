using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class Reactor : Node
{
	private Water[] _waterNodes;

	[Export] public double Health { get; set; } = 1.0;


	public override void _Ready()
	{
		_waterNodes = GetWaterNodes()
			.ToArray();
	}

	public override void _Process(double delta)
	{
		foreach (var a in GetParent()
            .GetChildren())
		{
			GD.Print(a.GetPath());
		}
    }

    public void CheckVictoryCondition()
    {
		CheckWaterAbsorption();
    }

    public void ApplyDamage(double damage)
    {
		Health -= damage;
		Health = Math.Max(Health, 0);
		CheckHealthDeathCondition();
    }

	private IEnumerable<Water> GetWaterNodes()
	{
		return GetParent()
			.GetNode("configuration")
			.GetChildren()
			.Where(x => x.Name.ToString().StartsWith("water"))
			.Cast<Water>()
			.Where(x => x != null);
	}

    private void CheckWaterAbsorption()
	{
		var win = true;
		foreach (var water in GetWaterNodes())
		{
			if (water.Remaining > 0)
			{
				win = false;
			}
		}

		if (win)
		{
			Win();
		}
	}

	private void CheckHealthDeathCondition()
	{
		if (Health <= 0)
		{
			CallDeferred("Die");
		}
	}

	private void Win()
    {
        var levelManager = GetTree().Root
            .GetNode<LevelManager>("./LevelManager");
        levelManager.SetActiveScene("res://Scenes/menu.tscn");
    }

	private void Die()
	{
        var levelManager = GetTree().Root
            .GetNode<LevelManager>("./LevelManager");
        levelManager.SetActiveScene("res://Scenes/menu.tscn");
    }
}
