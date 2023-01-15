using Godot;
using System;

public partial class Water : StaticBody3D
{
    private Reactor _reactor;

    public override void _Ready()
    {
        _reactor = GetNode<Reactor>("../../reactor");
        var area3D = GetNode<Area3D>("./Area3D");
        area3D.BodyEntered += OnBodyEntered;
    }

	private void OnBodyEntered(Node3D body)
    {
        if (body is Enemy enemy)
        {
            GainHeat(enemy);
        }
    }

    private void GainHeat(Enemy enemy)
    {
        enemy.QueueFree();
        _reactor.ApplyHeat(1.0);
    }
}
