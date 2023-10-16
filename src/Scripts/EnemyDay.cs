using Godot;
using System;

public partial class EnemyDay : CharacterBody2D
{
	[Export]
	public int Health { get; set; } = 3;
	public float Speed = 3;
	public Vector2 velocity { get; set; } = Vector2.Zero;

	public Area2D player;
	public AnimatedSprite2D sprite;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		sprite.Play();
		sprite.Animation = "walk";
	}
	
	public override void _PhysicsProcess(double delta)
	{
		sprite.FlipH = Position.X > player.Position.X;
		velocity = GlobalPosition.DirectionTo(player.GlobalPosition);
		MoveAndCollide(velocity * Speed);
	}
}
