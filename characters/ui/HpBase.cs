using Godot;
using System;

public partial class HpBase : Sprite2D
{
    public string NameClass { get; set; }
    private Character _character;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _character = GetParent<Character>();
        GetNameClass();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
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
        float n = 60;
        if (_character.HaveMana == true)
        {
            n = 60;
        } else
        {
            n = 40;
        }

        var node = GetNode<AnimatedSprite2D>("../" + NameClass + "/AnimatedSprite2D");
        var x = (float)node.Call("GetPosX");
        var y = (float)node.Call("GetPosY");
        var sizeY = (float)node.Call("GetSizeY");
        Position = new Vector2(x, y -(sizeY / 2) - n);
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
