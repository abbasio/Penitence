using Godot;
using System;

public partial class Gun : Sprite2D
{
	[Export]
	public double FireRate { get; set; } = 0.5;
	[Export]
	public int Damage { get; set; } = 1;
	[Export]
	public PackedScene BulletScene { get; set; }

	public bool canFire { get; set; } = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void shoot()
	{	
		canFire = false;
		GetNode<Timer>("FireRate").Start(FireRate);
		
		var fireEffect = GetNode<AnimatedSprite2D>("BulletFire");
		fireEffect.Animation = "fire";
		fireEffect.Frame = 0;
		fireEffect.Play();

		Bullet bullet = BulletScene.Instantiate<Bullet>();

		var levelRoot = GetParent();
		bullet.fire(fireEffect.GlobalPosition, Rotation);
		
		levelRoot.AddChild(bullet);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		LookAt(GetGlobalMousePosition());
		FlipV = GetGlobalMousePosition().X < Position.X;
		if (Input.IsActionPressed("shoot") && canFire)
		{	
			shoot();
		}
	}

	private void _on_fire_rate_timeout()
	{
		canFire = true;
	}
}
