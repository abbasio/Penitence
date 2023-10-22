using Godot;
using System;

public partial class Soul : Area2D
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
	private void _on_body_entered(Enemy enemy)
	{
		
		enemy.QueueFree();
    	this.Health -= 10; 
		if (this.Health <= 0)
		{
			QueueFree();
		}
		
	}

	
}
