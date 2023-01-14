using Godot;
using System;

public partial class Emitter : StaticBody3D
{
    private static PackedScene PackedEnemyScene = ResourceLoader.Load<PackedScene>("res://Scenes/enemy.tscn");
    [Export] public Vector2i Direction { get; set; }
	private double _count = 0;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		_count += delta * 0.2;
		if (_count > 1)
		{
			_count -= 1;
			//if (new Random().Next(10) == 0)
			{
				Spawn();
			}
		}
	}

	private void Spawn()
	{
		var enemy = PackedEnemyScene.Instantiate<Enemy>();
        enemy.Velocity = new Vector3(Direction.x, 0, Direction.y) * 2;
        GetParent().GetParent().AddChild(enemy, true);
        enemy.GlobalPosition = GlobalPosition + Vector3.Up * 0.5f;
    }
}