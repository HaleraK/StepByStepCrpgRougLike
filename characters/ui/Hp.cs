using Godot;
using System;

public partial class Hp : Sprite2D
{
    private string _name = "";
    private string _parentName = "";
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        GetName();
        SetSizeX();

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
        var node = GetNode("../HpBase");
        var x = (float)node.Call("GetPosX");
        var y = (float)node.Call("GetPosY");
        var sizeX = (float)node.Call("GetSizeX");
        var sizeY = (float)node.Call("GetSizeY");
        Position = new Vector2(x -(sizeX/2),y -(sizeY/2));
    }

    public void SetSizeX()
    {
        var parent = GetParent().Name;
        var hp = (float)GetNode("../../" + parent).Get("_hp");
        var hpMax = (float)GetNode("../../" + parent).Get("_hpMax");
        var size = hp/hpMax;

        Scale = new Vector2(200 * size, Scale.Y);
    }
}
