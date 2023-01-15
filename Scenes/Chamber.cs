using Godot;
using System;

public partial class Chamber : StaticBody3D
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
            TakeDamage(enemy);
        }
    }

    private void TakeDamage(Enemy enemy)
    {
        enemy.QueueFree();
        _reactor.ApplyDamage(1.0);
    }
}
