using Godot;
using System;
using static Godot.GD;
public partial class Daytime : Node
{
	[Export]
	public PackedScene EnemyDayScene { get; set; }

	public Vector2 playerPosition;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Timer>("EnemyTimer").Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var gun = GetNode<Sprite2D>("Gun");
		var player = GetNode<Area2D>("PlayerDay");

		gun.Position = player.Position;
		playerPosition = player.Position;
	}
	private void _on_enemy_timer_timeout()
	{
    	// Create a new instance of the Enemy scene.
    	EnemyDay enemy = EnemyDayScene.Instantiate<EnemyDay>();

    	// Choose a random location on Path2D.
    	var enemySpawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation");
		var player = GetNode<Area2D>("PlayerDay");
    	enemySpawnLocation.ProgressRatio = Randf();
		
		enemy.player = player;

    	// Set the mob's position to a random location.
    	enemy.Position = enemySpawnLocation.Position;

    	// Spawn the mob by adding it to the Main scene.
    	AddChild(enemy);
	}
	
}
