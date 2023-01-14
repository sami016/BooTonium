using Godot;
using System;

public partial class Enemy : CharacterBody3D
{
	private static Random Random = new Random();
	private Node3D _ghostModel;
	private double _count;

	public override void _Ready()
	{
		_ghostModel = GetNode<Node3D>("./ghost");
	}

	public override void _Process(double delta)
	{
		_count += delta;
		_ghostModel.Position = Vector3.Up * (float)Math.Cos(delta * 0.1);
	}

	public override void _PhysicsProcess(double delta)
	{
		FixVelocityToXZ();
		MoveAndSlide();
		if (Velocity.LengthSquared() > 0)
		{
			LookAt(new Vector3(GlobalPosition.x + Velocity.x, GlobalPosition.y, GlobalPosition.z + Velocity.z));
		}
    }

	private void FixVelocityToXZ()
	{
		var vel = Velocity;
		if (Math.Abs(vel.x) < 0.001 || Math.Abs(vel.z) < 0.001)
		{
			return;
		}
		var speed = vel.Length();
        if (vel.x > vel.z)
        {
            vel = new Vector3(speed, 0, 0);
        }
        else if (vel.z > vel.x)
        {
            vel = new Vector3(0, 0, speed);
        }
		else
		{
			vel = Random.Next(2) == 0
				? new Vector3(speed, 0, 0)
				: new Vector3(0, 0, speed);
        }
		Velocity = vel;
    }
}
