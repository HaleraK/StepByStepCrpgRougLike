using Godot;
using System;

public partial class InitBase : Sprite2D
{
    private string _name = "";
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        GetName();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void GetName()
    {
        var parent = GetParent().Name;
        _name = (string)GetNode("../../" + parent).Get("_name");
    }

    public void SetPosition()
    {
        var node = GetNode("../" + _name + "/AnimatedSprite2D");
        var x = (float)node.Call("GetPosX");
        var y = (float)node.Call("GetPosY");
        var sizeX = (float)node.Call("GetSizeX");
        var sizeY = (float)node.Call("GetSizeY");
        Position = new Vector2(x, y - (sizeY / 2) - 20);
    }

    public float GetSizeX()
    {
        return Scale.X;
    }

    public float GetSizeY()
    {
        return Scale.Y;
    }

    public float GetPosX()
    {
        return Position.X;
    }

    public float GetPosY()
    {
        return Position.Y;
    }
}
