using Godot;
using System;

public partial class ButtonAbility : Button
{
	[Export] private string _type = "";
	private readonly static string[] POSIBLE_TYPES
		= { "NormalAtk", "Ability", "Ability2", "Ability3", "Ability4" };
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnPressed()
	{
		GD.Print("click");
	}
}
