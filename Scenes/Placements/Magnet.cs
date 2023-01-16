using Godot;
using System;

public partial class Magnet : Node3D, IInteractable, IRotatable
{
    private MeshInstance3D _icon;
    private float _polarity = 1f;

    public string InteractDescription { get; } = "Change field direction";

    public override void _Ready()
    {
        _icon = GetNode<MeshInstance3D>("./icon");
        var area3D = GetNode<Area3D>("./Area3D");
        area3D.BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is Enemy enemy)
        {
            ChangeVelocity(enemy);
        }
    }

    private void ChangeVelocity(Enemy enemy)
    {
        enemy.ChangeDirectionFromField(this, _icon.GlobalTransform.basis.z);
    }

    public void Interact()
    {
        _icon.RotationDegrees += new Vector3(0, 90, 0);
    }

    public void Rotate()
    {
        RotationDegrees += new Vector3(0, 90, 0);
        _icon.RotationDegrees -= new Vector3(0, 90, 0);
    }
}
