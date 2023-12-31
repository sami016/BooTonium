using Godot;
using System;

public partial class PlayerBuildPlacement : Node
{
	public Placement Target { get; set; }
    public PlacementType Type { get; set; } = PlacementType.Magnet;

    public event Action TypeChanged;

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("item_1"))
        {
            SwitchItem(PlacementType.Magnet);
        }
        if (Input.IsActionJustPressed("item_2"))
        {
            SwitchItem(PlacementType.Accelerator);
        }
        if (Input.IsActionJustPressed("item_3"))
        {
            SwitchItem(PlacementType.Reflector);
        }
        if (Input.IsActionJustPressed("use")
            && CanPlace())
        {
            Place();
        }
        if (Input.IsActionJustPressed("interact")
            && CanInteract())
        {
            Interact();
        }
        if (Input.IsActionJustPressed("destroy")
            && CanDestroy())
        {
            Destroy();
        }
        if (Input.IsActionJustPressed("rotate")
            && CanRotate())
        {
            Rotate();
        }
    }

    private void SwitchItem(PlacementType placementType)
    {
        Type = placementType;
        TypeChanged?.Invoke();
    }

    private bool CanPlace()
    {
        return Target != null
            && Target.IsAvailable;
    }

    private bool CanInteract()
    {
        return Target != null
            && !Target.IsAvailable;
    }

    private bool CanDestroy()
    {
        return Target != null
            && !Target.IsAvailable;
    }

    private bool CanRotate()
    {
        return Target != null
            && !Target.IsAvailable;
    }

    public void Destroy()
    {
        Target.Destroy();
    }

    private void Place()
    {
        var playerRotationY = (GetParent() as player).RotationDegrees.y;
        playerRotationY = (float)Math.Round(playerRotationY / 90f) * 90f;
        Target.Place(Type, playerRotationY);
    }

    private void Rotate()
    {
        Target.Rotate();
    }

    private void Interact()
    {
        Target.Interact();
    }
}
