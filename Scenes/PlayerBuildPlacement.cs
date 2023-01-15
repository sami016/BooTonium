using Godot;
using System;

public partial class PlayerBuildPlacement : Node
{
	public Placement Target { get; set; }

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("use")
            && CanPlace())
        {
            Place();
        }
        if (Input.IsActionJustPressed("rotate")
            && CanRotate())
        {
            Rotate();
        }
    }

    private bool CanPlace()
    {
        return Target != null
            && Target.IsAvailable;
    }



    private bool CanRotate()
    {
        return Target != null
            && !Target.IsAvailable;
    }

    private void Place()
    {
        Target.Place(PlacementType.Magnet);
    }

    private void Rotate()
    {
        Target.Rotate();
    }
}
