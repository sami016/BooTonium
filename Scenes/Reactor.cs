using Godot;
using System;

public partial class Reactor : Node
{
    [Export] public double Health { get; set; } = 1.0;
    [Export] public double Temperature { get; set; } = 0.0;
	[Export] public double CoolingCoefficient { get; set; } = .002;
	[Export] public double TemperateRequirement { get; set; } = 10.0;

	public override void _Process(double delta)
	{
		Temperature -= CoolingCoefficient * delta;
		Temperature = Math.Max(Temperature, 0);
		CheckTemperatureWinCondition();
    }

    public void ApplyHeat(double heat)
    {
        Temperature += heat;
    }

    public void ApplyDamage(double damage)
    {
		Health -= damage;
		Health = Math.Max(Health, 0);
		CheckHealthDeathCondition();
    }

    private void CheckTemperatureWinCondition()
	{
		if (TemperateRequirement <= 0)
		{
			return;
		}

		if (Temperature >= TemperateRequirement)
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
