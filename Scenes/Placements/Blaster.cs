using Godot;
using System;
using System.Linq;

public partial class Blaster : Node3D
{
	private double _count = 0.0;
	[Export] public float Range { get; set; } = .0f;
	public Node3D Target { get; private set; }

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		_count += delta;
		if (_count > 0.5)
		{
			Sweep();
			_count = 0;
		}

		if (Target != null)
		{
			LookAt(new Vector3(Target.GlobalPosition.x, GlobalPosition.y, Target.GlobalPosition.z));
		}
	}

	private void Sweep()
	{
		var enemies = GetParent()
			.GetChildren()
			.Where(x => x.Name.ToString().StartsWith("enemy"))
			.Cast<Node3D>();
		foreach (var enemy in enemies)
		{
            GD.Print((enemy.GlobalPosition - GlobalPosition).Length());

            if ((enemy.GlobalPosition - GlobalPosition).LengthSquared() < Range * Range)
			{
				Target = enemy;
			}
		}
	}
}
