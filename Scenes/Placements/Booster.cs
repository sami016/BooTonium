using Godot;
using System;

public partial class Booster : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var area3D = GetNode<Area3D>("./Area3D");
		area3D.BodyEntered += OnBodyEntered;
    }

	private void OnBodyEntered(Node3D body)
    {
        GD.Print("body entered");
		GD.Print(body.GetPath());
		if (body is player ply)
		{
			ply.Stats.Apply(new StatEffect
			{
				Remaining = 5.0,
				Type = StatType.MoveSpeed,
				Add = 10,
                UniqueBuffType = UniqueBuffType.PlaceableSpeedBoost
            });
		}
    }


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
