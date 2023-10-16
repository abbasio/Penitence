using Godot;
using System;
using static Godot.GD;

public partial class Main : Node
{
	[Export]
	public PackedScene EnemyScene { get; set; }
	public Vector2 spirePosition;
	private int _score;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Timer>("EnemyTimer").Start();
		spirePosition = GetNode<Spire>("Spire").Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_enemy_timer_timeout()
	{
    	// Create a new instance of the Enemy scene.
    	Enemy enemy = EnemyScene.Instantiate<Enemy>();

    	// Choose a random location on Path2D.
    	var enemySpawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation");
    	enemySpawnLocation.ProgressRatio = Randf();

		if (enemySpawnLocation.Position.X > spirePosition.X)
		{
			enemy.GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = true;
		}

		// Choose the velocity.
		enemy.LinearVelocity = (spirePosition - enemySpawnLocation.Position) * enemy.Speed;

    	// Set the mob's position to a random location.
    	enemy.Position = enemySpawnLocation.Position;

    	// Spawn the mob by adding it to the Main scene.
    	AddChild(enemy);
	}
}
