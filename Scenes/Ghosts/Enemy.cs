using Godot;
using System;

public partial class Enemy : CharacterBody3D
{
	[Export] public GhostType Type { get; set; }
	[Export] public float Speed { get; set; } = 3f;

    public TrackPosition TrackPosition { get; set; } = TrackPosition.Starboard;

    private Node3D _ghostModel;
	private double _count;

	private Vector3 _direction;

	private Node _lastDirectionFieldInfluence;

	public Vector3 Direction
	{
		get => _direction;
		set
		{
			_direction = value.Normalized();
		}
	}

	public override void _Ready()
	{
		_ghostModel = GetNode<Node3D>("./ghost");
		
	}

	public override void _Process(double delta)
	{
		_count += delta;
		_ghostModel.Position = Vector3.Up * (float)Math.Cos(_count * 0.1);
	}

	public override void _PhysicsProcess(double delta)
    {
        var trackAdjustVelocity = GetTrackVelocityAdjust();
        Velocity = _direction * Speed + trackAdjustVelocity;
		var preMovePosition = Position;
		var hasCollided = MoveAndSlide();
		if (hasCollided)
		{
			var collision = GetSlideCollision(0);
			CollideRotate(preMovePosition);
		}
		if (Velocity.LengthSquared() > 0)
		{
			LookAt(new Vector3(GlobalPosition.x + _direction.x, GlobalPosition.y, GlobalPosition.z + _direction.z));
		}
    }

	private void CollideRotate(Vector3 preMovePosition)
	{
        Direction = GlobalTransform.basis.x * TrackPosition.GetPositionMultiplier();
        Position = preMovePosition;

        TrackPosition = TrackPosition.GetOpposite();
	}

	private Vector3 GetTrackVelocityAdjust()
	{
		var targetUnits = (float)(Math.Round(GlobalPosition.Dot(GlobalTransform.basis.x) / 8) * 8 + 2 * TrackPosition.GetPositionMultiplier());
        var diffUnits = targetUnits
            - (float)GlobalPosition.Dot(GlobalTransform.basis.x);


        var targetShift = GlobalTransform.basis.x * diffUnits;
		return targetShift * 2.5f;

    }

	public void ChangeDirectionFromField(Node lastDirectionFieldInfluence, Vector3 direction)
	{
		if (_lastDirectionFieldInfluence == lastDirectionFieldInfluence)
		{
			return;
		}
		Direction = direction;
		_lastDirectionFieldInfluence = lastDirectionFieldInfluence;
    }
}