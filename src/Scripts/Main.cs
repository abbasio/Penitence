using Godot;
using System;
using System.Collections.Generic;
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
	public int dayCount = 0;

	public Player Player;

	public Gun Gun;

	public bool isDay = true;
	public int enemySpeedModifier = 0;
	public int bodySpeedModifier = 0;
	public int soulSpeedModifier = 0;
	public float dayTimerWaitTime = 10;
	public float nightTimerWaitTime = 10;
	public float enemyTimerDay = 1;
	public float enemyTimerNight = 1;
	public 

	class Choice
    {
        public string Day { get; set; }
        public string Night { get; set; }
    }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Control>("Choice").Hide();
		Player =  GetNode<Player>("Player");
		Gun = GetNode<Gun>("Gun");
		Gun.player = Player;
		enemyTimer = GetNode<Timer>("EnemyTimer");
		dayTimer = GetNode<Timer>("DayTimer");
		dayTimer.WaitTime = dayTimerWaitTime;
		nightTimer = GetNode<Timer>("NightTimer");
		nightTimer.WaitTime = nightTimerWaitTime;	
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
			if (!HasNode(GetPathTo(Gun))) AddChild(Gun);
			if (dayCount > 0) choosePowerup();
			enemyTimer.WaitTime = enemyTimerDay;
			GD.Print(Gun.Damage);
			GD.Print(enemyTimer.WaitTime);
		}
		else
		{
			Player.becomeNight();
			RemoveChild(Gun);
			enemyTimer.WaitTime = enemyTimerNight;
			GD.Print(enemyTimer.WaitTime);
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
		dayCount++;
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
	public void choosePowerup()
	{
		var Choices = new Dictionary<int, Choice>()
        {
            { 1, new Choice 
                { 
                    Day="BODY\n By day, you do more damage to the undead\nBy night, the spirits increase in number...", 
                    Night="SOUL\n By night, your spirit moves with greater haste\n By day, the undead grow stronger..."
                }
            },
        };
		Button dayButton = (Button)GetNode<Control>("Choice").FindChild("Day", true);
		dayButton.Text = Choices[dayCount].Day;
		dayButton.Pressed += dayOneBody;
		Button nightButton = (Button)GetNode<Control>("Choice").FindChild("Night", true);
		nightButton.Text = Choices[dayCount].Night;
		GetTree().Paused = true;
		GD.Print("Game Paused until power is selected");
		GetNode<Control>("Choice").Show();
	}
	class Consequence
	{
		public delegate void Body();
		public delegate void Soul();
	}

	private void dayOneBody()
	{
		GD.Print("gun does more damage, enemies spawn faster at night");
		Gun.Damage = 2;
		enemyTimerNight = (float)0.5;
	}
}
