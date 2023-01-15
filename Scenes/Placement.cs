using Godot;
using System;
using System.Collections.Generic;

public partial class Placement : Area3D
{
	private static IDictionary<PlacementType, PackedScene> _scenes = new Dictionary<PlacementType, PackedScene>()
    {
        { PlacementType.SpeedBooster, ResourceLoader.Load<PackedScene>("res://Scenes/Placements/booster.tscn") },

        { PlacementType.Blaster, ResourceLoader.Load<PackedScene>("res://Scenes/Placements/blaster.tscn") },
		{ PlacementType.Magnet, ResourceLoader.Load<PackedScene>("res://Scenes/Placements/magnet.tscn")  }
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

	public void Place(PlacementType placementType)
	{
		if (!IsAvailable)
		{
			return;
		}

		_typePlaced = placementType;

        CreatePlacement();
	}

	private void CreatePlacement()
	{
		var placement = _scenes[_typePlaced.Value].Instantiate<Node3D>();
		GetParent().GetParent().GetParent().AddChild(placement, true);
		placement.GlobalTransform = GlobalTransform;
		_currentPlacement = placement;
    }

    internal void Rotate()
    {
        _currentPlacement.RotationDegrees += new Vector3(0, 90, 0);
    }
}
