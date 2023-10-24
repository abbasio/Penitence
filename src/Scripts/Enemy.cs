using Godot;
using System;
using static Global.Utilities;

public partial class Enemy : CharacterBody2D
{
	[Export]
	public int Health { get; set; } = 3;
	public float Speed = 2;
	public Vector2 velocity { get; set; } = Vector2.Zero;

	public Area2D target;
	public AnimatedSprite2D sprite;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		sprite.Play();
		sprite.Animation = "walk";
	}
	
	public override void _PhysicsProcess(double delta)
	{
		sprite.FlipH = Position.X > target.Position.X;
		velocity = GlobalPosition.DirectionTo(target.GlobalPosition);
		MoveAndCollide(velocity * Speed);

		if (isAnimationOver(sprite, "hit"))
		{
			sprite.Animation = "walk";
		}

		if (isAnimationOver(sprite, "die"))
		{
			QueueFree();
		}
	}
	public void hit(Bullet bullet)
	{
		sprite.Animation = ("hit");
		bullet.QueueFree();
		Health -= bullet.Damage;
		if (Health <= 0) die();
	}
	public void die()
	{
		Speed = 0;
		sprite.Animation = ("die");
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}

}
