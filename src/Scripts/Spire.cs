using Godot;
using System;

public partial class Spire : Area2D
{
	[Signal]
	public delegate void HitEventHandler();
	[Export]
	public int Health { get; set; } = 100;
	public Vector2 ScreenSize;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		this.Position = new Vector2 (ScreenSize.X / 2, ScreenSize.Y / 2);
		Start(this.Position);
	}

	public void Start(Vector2 position)
	{
		this.Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_body_entered(RigidBody2D enemy)
	{
		enemy.LinearVelocity = Vector2.Zero;
		enemy.GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
		var sprite = enemy.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		sprite.Animation = "attack";
    	this.Health -= 10; 
    	GD.Print(this.Health);
    	
		if (this.Health <= 0)
		{
			QueueFree();
		}
		
	}

	
}
