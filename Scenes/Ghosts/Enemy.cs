using Godot;
using System;

public partial class Enemy : CharacterBody3D
{
    private static PackedScene redPackedScene = ResourceLoader.Load<PackedScene>("res://Scenes/Ghosts/red.tscn");
    private static PackedScene greenPackedScene = ResourceLoader.Load<PackedScene>("res://Scenes/Ghosts/green.tscn");

    [Export] public GhostType Type { get; set; }
	[Export] public float Speed { get; set; } = 3f;

    public TrackPosition TrackPosition { get; set; } = TrackPosition.Starboard;

    private Node3D _ghostModel;
	private double _count;

	private Vector3 _direction;

	private Node _lastDirectionFieldInfluence;

    protected bool HasFused;

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
        var area3D = GetNode<Area3D>("./Area3D");
        area3D.BodyEntered += OnBodyEntered;

    }

	public override void _Process(double delta)
	{
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
		
		//_count += delta;
		//_ghostModel.Position = Vector3.Up * (float)Math.Cos(_count * 0.1f) * 10f;
		//GD.Print(_ghostModel.Position);
        if (Velocity.LengthSquared() > 0)
		{
			LookAt(new Vector3(GlobalPosition.x + _direction.x, GlobalPosition.y, GlobalPosition.z + _direction.z));
		}
    }

	private void CollideRotate(Vector3 preMovePosition)
	{
		var oldDirection = Direction;
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

		var oldDirection = Direction;
        Direction = direction;

        GD.Print("dot " + oldDirection.Dot(Direction));
		GD.Print(TrackPosition);
        if (oldDirection.Dot(Direction) < 0)
        {
            TrackPosition = TrackPosition.GetOpposite();
        }
        GD.Print(TrackPosition);

        _lastDirectionFieldInfluence = lastDirectionFieldInfluence;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is Enemy enemy)
        {
            HandleEnemyCollision(enemy);
        }
    }

    private void HandleEnemyCollision(Enemy enemy)
    {
		if (enemy == this)
		{
			return;
		}
		if (enemy.Type == GhostType.Blue && Type == GhostType.Blue)
		{
            var dotProct = enemy.Direction.Dot(Direction);
			if (dotProct < 0)
			{
				FuseWith(enemy);
			}
		}
    }

	private void FuseWith(Enemy enemy)
	{
		if (HasFused || enemy.HasFused)
		{
			return;
		}
		QueueFree();
		enemy.QueueFree();

		var position = (Position + enemy.Position) / 2;
		var red = redPackedScene.Instantiate<Enemy>();
		red.Position = position;
		red.Direction = enemy.Direction;
		var green = greenPackedScene.Instantiate<Enemy>();
		green.Position = Position;
		green.Direction = Direction;

        GetParent().AddChild(red, true);
        GetParent().AddChild(green, true);
		HasFused = true;
    }
}
