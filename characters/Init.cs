using Godot;
using System;

public partial class Init : Sprite2D
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
        _name = (string)GetNode("../../Character").Get("_name");
    }

    public void SetPosition()
    {
        var node = GetNode("../InitBase");
        var x = (float)node.Call("GetPosX");
        var y = (float)node.Call("GetPosY");
        var sizeX = (float)node.Call("GetSizeX");
        var sizeY = (float)node.Call("GetSizeY");
        Position = new Vector2(x - (sizeX / 2), y - (sizeY / 2));
    }

    public void SetSizeX()
    {
        var init = (float)GetNode("../../Character").Get("_initiativeCurrent");
        var initMax = (float)GetNode("../../Character").Get("_initiativeMax");
        var size = init / initMax;

        Scale = new Vector2(200 * size, Scale.Y);
    }
}
