using Godot;
using System;

public partial class Filter : Area3D
{
    [Export] public GhostType GhostType { get; set; }

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is Enemy enemy)
        {
            if (enemy.Type != GhostType)
            {
                enemy.Explode();
            }
        }
    }

}
