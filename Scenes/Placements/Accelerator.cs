using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Accelerator : Node3D
{
    private static IDictionary<GhostType, PackedScene> AcceleratorMap = new Dictionary<GhostType, PackedScene>()
    {
        { GhostType.White, ResourceLoader.Load<PackedScene>("res://Scenes/Ghosts/blue.tscn") },
        { GhostType.Green, ResourceLoader.Load<PackedScene>("res://Scenes/Ghosts/green_plus.tscn") },
        { GhostType.Red, ResourceLoader.Load<PackedScene>("res://Scenes/Ghosts/red_plus.tscn") },
    };

    public override void _Ready()
    {
        var area3D = GetNode<Area3D>("./Area3D");
        area3D.BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is Enemy enemy)
        {
            if (!AcceleratorMap.ContainsKey(enemy.Type)
                || enemy.Age < 0.5)
            {
                return;
            }

            if ((GlobalTransform.basis.z).Dot(enemy.Direction) < 0.95)
            {
                return;
            }


            var upgraded = AcceleratorMap[enemy.Type].Instantiate<Enemy>();
            upgraded.Direction = enemy.Direction;
            upgraded.Position = enemy.Position;
            upgraded.TrackPosition = enemy.TrackPosition;
            GetParent().GetParent().AddChild(upgraded, true);
            enemy.QueueFree();
        }
    }

}
