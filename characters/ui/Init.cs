using Godot;
using System;

public partial class Init : Sprite2D
{
    public string NameClass { get; set; }
    private Character _character;
    private InitBase _initBase;
    public override void _Ready()
	{
        _character = GetParent<Character>();
        _initBase = GetNode<InitBase>("../InitBase");
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
        var x = (float)_initBase.Call("GetPosX");
        var y = (float)_initBase.Call("GetPosY");
        var sizeX = (float)_initBase.Call("GetSizeX");
        var sizeY = (float)_initBase.Call("GetSizeY");
        Position = new Vector2(x - (sizeX / 2), y - (sizeY / 2));
    }

    public void SetSizeX()
    {
        var init = (float)_character.InitiativeCurrent;
        var initMax = (float)_character.InitiativeMax;
        var size = init / initMax;

        Scale = new Vector2(200 * size, Scale.Y);
    }
}
