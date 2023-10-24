using Godot;
using System;

public partial class Bullet : Area2D
{
	public float Speed = 2;
	public Vector2 velocity { get; set; } = Vector2.Zero;
	public int Damage = 0;
	public void fire(Vector2 position, float direction)
	{
		Position = position;
		Rotation = direction;
		velocity = new Vector2(Speed, 0).Rotated(Rotation);
	}
	public override void _Ready()
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play();
		animatedSprite2D.Animation = "shoot";
	}
	public override void _Process(double delta)
	{
		Position += velocity * Speed;
	}

	private void _on_body_entered(Enemy enemy)
	{
    	enemy.hit(this);
	}

	private void _on_visible_on_screen_notifier_2d_screen_exited()
	{
		QueueFree();
	}
}
