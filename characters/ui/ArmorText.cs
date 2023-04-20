using Godot;
using System;

public partial class ArmorText : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        //GD.Print(Name);
        SetArmor();
		SetPosition();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
    }

	public void SetArmor()
	{
        var parent = GetParent().Name;
        Text = (string)GetNode("../../" + parent).Get("_armor");
	}

    public void SetPosition()
    {
        var node = GetNode("../HpBase");
        var x = (float)node.Call("GetPosX");
        var y = (float)node.Call("GetPosY");
        var sizeX = (float)node.Call("GetSizeX");
        var sizeY = (float)node.Call("GetSizeY");
        Position = new Vector2(x + 10 + (sizeX / 2), y - 15);

    }
}
