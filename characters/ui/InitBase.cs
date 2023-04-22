using Godot;
using System;

public partial class InitBase : Sprite2D
{
    public string NameClass { get; set; }
    private Character _character;
    public override void _Ready()
	{
        _character = GetParent<Character>();
        GetNameClass();
    }

	public override void _Process(double delta)
	{
	}

    public void GetNameClass()
    {
        var parent = GetParent().Name;
        NameClass = _character.NameClass;
    }

    public void SetPosition()
    {
        var node = GetNode("../" + NameClass + "/AnimatedSprite2D");
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
