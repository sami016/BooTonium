using Godot;
using System;
using System.Security.Cryptography;

public partial class Turbine : Area3D
{
    private Reactor _reactor;

    public float RotationRate { get; set; } = 0f;
    private static float SlowRate { get; set; } = 0.01f;
    public bool Spinning => Math.Abs(RotationRate) > 90f;

    public override void _Ready()
    {
        _reactor = GetNode<Reactor>("../../reactor");
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is Enemy enemy)
        {
            RotationRate += GetClockwiseDirectionMultipler(enemy) * 18f;
            RotationRate = Math.Min(100, Math.Max(-100, RotationRate));

        }
    }

    private float GetClockwiseDirectionMultipler(Enemy enemy)
    {
        var pos = enemy.Position - GlobalPosition;
        var pos2 = pos + enemy.Direction * 0.1f;
        var cross = pos.Cross(pos2);
        var direction = Math.Sign(cross.Dot(Vector3.Up));
        return direction;
    }

    public override void _Process(double delta)
	{
        RotationRate -= Math.Sign(RotationRate) * SlowRate;
        RotationDegrees += new Vector3(0, (float)delta * RotationRate, 0);
        _reactor.CheckVictoryCondition();
	}
}
