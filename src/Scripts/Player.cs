using Godot;
using System;
using static Godot.GD;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 3;
	public int nightSpeed = 3;
	public int daySpeed = 3;
	[Export]
	public int Health { get; set; } = 100;
	public string Form = "day";

	public Vector2 ScreenSize;

	public AnimatedSprite2D sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
    	sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		Vector2 velocity = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		if (Form == "day")
		{
			if (velocity == Vector2.Zero)
			{
				sprite.Animation = "idle";
			}
			else
			{
				sprite.Play();
				sprite.Animation = "walk_side";
				sprite.FlipH = velocity.X < 0;
			}

		}

		if (Form == "night")
		{
			sprite.Animation = "idle_night";
		}

		this.Position += velocity * Speed;
		this.Position = new Vector2(
    		x: Mathf.Clamp(Position.X , 30, ScreenSize.X - 30),
    		y: Mathf.Clamp(Position.Y, 20, ScreenSize.Y - 40)
		);

		if (Health <= 0)
		{
			die();
		}
	
	}

	public void becomeNight()
	{
		Form = "night";
		Speed = nightSpeed;
	}

	public void becomeDay()
	{
		Form = "day";
		Speed = daySpeed;
	}

	private void _on_body_entered(Enemy enemy)
	{
		if (Form == "night")
		{
			enemy.die();
		}
		if (Form == "day")
		{
			GD.Print(enemy.Damage);
			Health -= enemy.Damage;
		}
	}

	public void die()
	{
		QueueFree();
	}
}