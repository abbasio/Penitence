using Godot;
using System;
using static Godot.GD;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 5;
	[Export]
	public int Health { get; set; } = 100;

	public Vector2 ScreenSize;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
    	var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Vector2 velocity = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		if (velocity == Vector2.Zero)
		{
			animatedSprite2D.Stop();
			animatedSprite2D.Animation = "idle";
		}
		else
		{
			animatedSprite2D.Play();
			animatedSprite2D.Animation = "walk";
			animatedSprite2D.FlipH = velocity.X < 0;
		}

		this.Position += velocity * Speed;
		this.Position = new Vector2(
    		x: Mathf.Clamp(Position.X , 30, ScreenSize.X - 30),
    		y: Mathf.Clamp(Position.Y, 20, ScreenSize.Y - 40)
		);
	
	}
	private void kill(RigidBody2D enemy)
	{
		enemy.GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
		enemy.LinearVelocity = Vector2.Zero;
		var sprite = enemy.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		sprite.Animation = "die";
	}
	private void _on_body_entered(RigidBody2D enemy)
	{
    	kill(enemy);
	}
}

/* 
Scope for project

Wave based defense, divided into 'days'. Each day contains a morning and night portion.
In the morning, zombies come to try and eat your brains. You will control your own body to fight them off, with a basic melee attack combo. (maybe ranged?)
At night time, reapers come to try and harvest your soul. You control your spirit to defend yourself, defeating enemies by flying into them
After a full day (morning + night cycle) you will get the opportunity to choose an upgrade. For example, more damage for your body or faster movement for the soul
Picking one option will increase the difficulty of the opposite. Ie; faster soul but daytime zombies have more health
Game goes on for 7 full days (7 mornings and 7 nights) before ending. Mb a small story attached


*/