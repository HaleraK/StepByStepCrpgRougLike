using Godot;
using System;

public partial class AnimatedSprite2D : Godot.AnimatedSprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetSizeY();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public float GetSizeY() 
	{
        var size = SpriteFrames.GetFrameTexture("default", 0).GetSize();
		return size.Y/10;
    }

    public float GetSizeX()
    {
        var size = SpriteFrames.GetFrameTexture("default", 0).GetSize();
        return size.X/10;
    }
}
