using Godot;
using System;

public partial class Accelerator : Node3D
{
    private static PackedScene BluePackedScene = ResourceLoader.Load<PackedScene>("res://Scenes/Ghosts/blue.tscn");

    public override void _Ready()
    {
        var area3D = GetNode<Area3D>("./Area3D");
        area3D.BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is Enemy enemy)
        {
            if (enemy.Type != GhostType.White)
            {
                return;
            }

            if ((GlobalTransform.basis.z).Dot(enemy.Direction) < 0)
            {
                return;
            }


            var upgraded = BluePackedScene.Instantiate<Enemy>();
            upgraded.Direction = enemy.Direction;
            upgraded.Position = enemy.Position;
            upgraded.TrackPosition = enemy.TrackPosition;
            GetParent().GetParent().AddChild(upgraded, true);
            enemy.QueueFree();
        }
    }

}
