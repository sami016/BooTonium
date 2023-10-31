using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class Reactor : Node
{
    private Water[] _waterNodes;
    private Turbine[] _turbineNodes;
    private bool _finished = false;

    [Export] public double Health { get; set; } = 1.0;
    public double CompletePercentage { get; set; } = 0;
    [Export] public int LevelIndex { get; set; } = 0;

    [Export] public Control WinWindow { get; set; }
    [Export] public Control LossWindow { get; set; }

    public event Action CompletePercentageChanged;

	public override void _Ready()
    {
        _waterNodes = GetWaterNodes()
            .ToArray();
        _turbineNodes = GetTurbineNodes()
            .ToArray();
    }

	public override void _Process(double delta)
    {
        if (_finished)
        {
            return;
        }
        UpdateProgress();
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

    private void UpdateProgress()
    {
        if (_finished)
        {
            return;
        }
        var numerator = 0.0;
        var denominator = 0.0;

        foreach (var node in _turbineNodes)
        {
            numerator += Math.Min(node.RotationRate, 80.0);
            denominator += 80.0;
        }
        foreach (var node in _waterNodes)
        {
            numerator += Math.Min(node.GhostsReceived, node.GhostsRequired);
            denominator += node.GhostsRequired;
        }

        if (denominator == 0)
        {
            CompletePercentage = 0;
        }
        else
        {
            CompletePercentage = 100 * numerator / denominator;
        }
        CompletePercentageChanged?.Invoke();
    }

    public void ApplyDamage(double damage)
    {
        if (_finished)
        {
            return;
        }
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
        _finished = true;
        WinManager.LevelWasWon(LevelIndex)
            .ContinueWith(x =>
            {
                CallDeferred("WinTransition");
            });
    }

    private void WinTransition()
    {
        WinWindow.Visible = true;
    }

	private void Die()
	{
        _finished = true;
        LossWindow.Visible = true;
    }
}
