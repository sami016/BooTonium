using Godot;
using System;

public partial class Magnet : Node3D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var area3D = GetNode<Area3D>("./Area3D");
        area3D.BodyEntered += OnBodyEntered;
        GD.Print("Magnet spawned");
    }

    private void OnBodyEntered(Node3D body)
    {
        GD.Print(body);
        if (body is Enemy enemy)
        {
            GD.Print("is enemy");
            ChangeVelocity(enemy);
        }
    }

    private void ChangeVelocity(Enemy enemy)
    {
        enemy.ChangeDirectionFromField(this, GlobalTransform.basis.z);
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
