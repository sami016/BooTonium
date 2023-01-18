using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class Reactor : Node
{
    private Water[] _waterNodes;
    private Turbine[] _turbineNodes;

    [Export] public double Health { get; set; } = 1.0;


	public override void _Ready()
    {
        _waterNodes = GetWaterNodes()
            .ToArray();
        _turbineNodes = GetTurbineNodes()
            .ToArray();
    }

	public override void _Process(double delta)
	{
    }

    public void CheckVictoryCondition()
    {
        var win = true;
        win &= CheckWaterAbsorption();
        win &= CheckTurbinesSpinning();

        if (win)
        {
            Win();
        }
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

    private IEnumerable<Turbine> GetTurbineNodes()
    {
        return GetParent()
            .GetNode("configuration")
            .GetChildren()
            .Where(x => x.Name.ToString().StartsWith("turbine"))
            .Cast<Turbine>()
            .Where(x => x != null);
    }

    private bool CheckWaterAbsorption()
    {
        var win = true;
        foreach (var water in _waterNodes)
        {
            if (water.Remaining > 0)
            {
                win = false;
            }
        }
        return win;
    }

    private bool CheckTurbinesSpinning()
    {
        var win = true;
        foreach (var turbine in _turbineNodes)
        {
            if (!turbine.Spinning)
            {
                win = false;
            }
        }
        return win;
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
