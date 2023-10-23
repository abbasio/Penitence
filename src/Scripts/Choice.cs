using Godot;
using System;

public partial class Choice : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ProcessMode = Node.ProcessModeEnum.WhenPaused;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_day_pressed()
	{
		GetTree().Paused = false;
		GD.Print("Day chosen");
		Hide();
	}
	private void _on_night_pressed()
	{
		GetTree().Paused = false;
		GD.Print("Night chosen");
		Hide();
	}
	
}
