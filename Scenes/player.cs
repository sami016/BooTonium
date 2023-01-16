using Godot;
using System;

public partial class player : CharacterBody3D
{
	public const float JumpVelocity = 4.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public Stats Stats { get; private set; }
    public PlayerBuildPlacement Placement { get; private set; }

    public override void _Ready()
    {
		Stats = GetNode<Stats>("./Stats");
        Placement = GetNode<PlayerBuildPlacement>("./Build");

		Stats.Apply(new StatEffect
		{
			Type = StatType.MoveSpeed,
			Add = 10,
			Permanent = true
		});
    }

    public override void _PhysicsProcess(double delta)
	{
		var moveSpeed = Stats.GetStat(StatType.MoveSpeed);
        Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.y -= gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		Vector3 direction = new Vector3(inputDir.x, 0, inputDir.y).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.x = direction.x * moveSpeed;
			velocity.z = direction.z * moveSpeed;
		}
		else
		{
			velocity.x = Mathf.MoveToward(Velocity.x, 0, moveSpeed);
			velocity.z = Mathf.MoveToward(Velocity.z, 0, moveSpeed);
		}

		Velocity = velocity;
        if (Velocity.LengthSquared() > 0.01)
        {
            LookAt(new Vector3(Position.x + Velocity.x, Position.y, Position.z + Velocity.z));
        }
        MoveAndSlide();
    }
}
