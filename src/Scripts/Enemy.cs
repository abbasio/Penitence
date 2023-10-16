using Godot;
using System;

public partial class Enemy : RigidBody2D
{
	public float Speed { get; set; } = 1;
	public float Health { get; set; } = 3;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play();
		animatedSprite2D.Animation = "walk";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_animated_sprite_2d_animation_finished()
	{
		QueueFree();
	}
	
}
