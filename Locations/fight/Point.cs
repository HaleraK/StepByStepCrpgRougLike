using Godot;
using System;

public partial class Point : Marker2D
{
	// Called when the node enters the scene tree for the first time.

	[Export] private string _type; //char or mob
	[Export] private string id;
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
