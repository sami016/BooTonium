using Godot;
using System;

public partial class Emitter : StaticBody3D
{
    private static PackedScene PackedEnemyScene = ResourceLoader.Load<PackedScene>("res://Scenes/Ghosts/white.tscn");
    [Export] public Vector2i Direction { get; set; }
	[Export] public TrackPosition TrackPosition { get; set; } = TrackPosition.Port;
	private double _count = 0;

	public override void _Ready()
	{
		_count = 1;
        var area3D = GetNode<Area3D>("./Area3D");
        area3D.BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is Enemy enemy)
        {
            if (enemy.Age > 1)
            {
                enemy.Explode();
            }
        }
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
        enemy.Direction = new Vector3(Direction.x, 0, Direction.y);
        enemy.Position = GlobalPosition + Vector3.Up * 2.5f;
        enemy.TrackPosition = TrackPosition;
        GetParent().GetParent().AddChild(enemy, true);
    }
}
