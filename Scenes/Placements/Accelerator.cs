using Godot;
using System;

public partial class Accelerator : Node3D
{
    public override void _Ready()
    {
        var area3D = GetNode<Area3D>("./Area3D");
        area3D.BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is Enemy enemy)
        {
            GD.Print("accelerating");
            enemy.Accelerate(this);
        }
    }

}
