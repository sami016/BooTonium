using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Stats : Node
{
	private IList<StatEffect> _statEffects = new List<StatEffect>();

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		foreach (var effect in _statEffects.ToArray())
		{
			if (effect.Permanent)
			{
				continue;
			}
			effect.Remaining -= delta;
			if (effect.Remaining <= 0)
			{
				_statEffects.Remove(effect);
			}
		}
	}

	public bool Apply(StatEffect statEffect)
	{
		if (statEffect.UniqueBuffType.HasValue
			&& _statEffects.Count(x => x.UniqueBuffType == statEffect.UniqueBuffType) > 0)
		{
			return false;
		}
		_statEffects.Add(statEffect);
		return true;
    }

    public float GetStat(StatType statType)
	{
		return _statEffects.Where(x => x.Type == statType)
			.Sum(x => x.Add);
	}
}
