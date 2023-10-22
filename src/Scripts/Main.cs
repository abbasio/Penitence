using Godot;
using System;
using static Godot.GD;

public partial class Main : Node
{
	[Export]
	public PackedScene EnemyScene { get; set; }	
	public Vector2 soulPosition;
	public Timer enemyTimer;
	public Timer dayTimer;
	public Timer nightTimer;
	public Timer readyTimer;

	public Player Player;

	public Gun Gun;

	public bool isDay = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player =  GetNode<Player>("Player");
		Gun = GetNode<Gun>("Gun");
		Gun.player = Player;
		enemyTimer = GetNode<Timer>("EnemyTimer");
		dayTimer = GetNode<Timer>("DayTimer");
		nightTimer = GetNode<Timer>("NightTimer");
		readyTimer = GetNode<Timer>("ReadyTimer");
		soulPosition = GetNode<Soul>("Soul").Position;
		readyTimer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_ready_timer_timeout()
	{
		readyTimer.Stop();
		Timer timerToUse = isDay ? dayTimer : nightTimer;
		if (isDay)
		{
			Player.becomeDay();
			AddChild(Gun);
		}
		else
		{
			Player.becomeNight();
			RemoveChild(Gun);
		}
		timerToUse.Start();
		enemyTimer.Start();	
	}

	private void _on_day_timer_timeout()
	{
		dayTimer.Stop();
		isDay = false;
		Print("Day done!");
		enemyTimer.Stop();
		readyTimer.Start();
	}
	private void _on_night_timer_timeout()
	{
		nightTimer.Stop();
		isDay = true;
		Print("Night done!");
		enemyTimer.Stop();
		readyTimer.Start();
	}
	private void spawnEnemy()
	{
		// Create a new instance of the Enemy scene.
    	Enemy enemy = EnemyScene.Instantiate<Enemy>();

    	// Choose a random location on Path2D.
    	var enemySpawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation");
		var player = GetNode<Area2D>("Player");
		var soul = GetNode<Area2D>("Soul");
    	enemySpawnLocation.ProgressRatio = Randf();
		
		enemy.target = isDay ? player : soul;

    	// Set the mob's position to a random location.
    	enemy.Position = enemySpawnLocation.Position;

    	// Spawn the mob by adding it to the Main scene.
    	AddChild(enemy);
	}

	private void _on_enemy_timer_timeout()
	{
    	spawnEnemy();
	}
}
