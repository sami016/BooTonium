using Godot;
using System;
using System.Collections.Generic;

public partial class Placement : Area3D
{
	private static IDictionary<PlacementType, PackedScene> _scenes = new Dictionary<PlacementType, PackedScene>()
    {
        { PlacementType.SpeedBooster, ResourceLoader.Load<PackedScene>("res://Scenes/Placements/booster.tscn") },

        { PlacementType.Blaster, ResourceLoader.Load<PackedScene>("res://Scenes/Placements/blaster.tscn") },
        { PlacementType.Magnet, ResourceLoader.Load<PackedScene>("res://Scenes/Placements/magnet.tscn")  },
        { PlacementType.Accelerator, ResourceLoader.Load<PackedScene>("res://Scenes/Placements/accelerator.tscn")  }
    };

	private PlacementType? _typePlaced;
	private Node3D _currentPlacement;
    public bool IsAvailable => !_typePlaced.HasValue;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}


    private void OnBodyEntered(Node3D body)
    {
		(body as player).Placement.Target = this;
	}

    private void OnBodyExited(Node3D body)
    {
        (body as player).Placement.Target = null;
    }

	public void Place(PlacementType placementType, float playerRotationY)
	{
		if (!IsAvailable)
		{
			return;
		}

		_typePlaced = placementType;

        CreatePlacement(playerRotationY);
	}

	private void CreatePlacement(float playerRotationY)
	{
		var placement = _scenes[_typePlaced.Value].Instantiate<Node3D>();
		GetParent().GetParent().GetParent().AddChild(placement, true);
		placement.GlobalTransform = GlobalTransform;
		placement.RotationDegrees = new Vector3(0, playerRotationY+180, 0);
        _currentPlacement = placement;
    }

    public void Rotate()
    {
        if (_currentPlacement is IRotatable rotatable)
        {
            rotatable.Rotate();
        }
        else
        {
            _currentPlacement.RotationDegrees += new Vector3(0, 90, 0);
        }
    }

	public void Destroy()
    {
        if (IsAvailable)
        {
            return;
        }

        _currentPlacement?.QueueFree();

        _typePlaced = null;
        _currentPlacement = null;
    }

    public void Interact()
    {
        if (_currentPlacement is IInteractable interactable)
        {
            interactable.Interact();
        }
    }
}
