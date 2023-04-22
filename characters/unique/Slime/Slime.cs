using Godot;
using StepByStepCrpgRougLike.characters;
using System;

public partial class Slime : Area2D
{
    private CharacterStats _stats;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    public void TougleVisible()
    {
        Visible = !Visible;
    }
}
