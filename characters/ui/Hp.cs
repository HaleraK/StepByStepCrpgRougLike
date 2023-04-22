using Godot;
using System;

public partial class Hp : Sprite2D
{
    public string NameClass { get; set; }
    private Character _character;
    private HpBase _hpBase;
    public override void _Ready()
	{
        _character = GetParent<Character>();
        _hpBase = GetNode<HpBase>("../HpBase");
        GetNameClass();
        SetSizeX();
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
        var x = (float)_hpBase.Call("GetPosX");
        var y = (float)_hpBase.Call("GetPosY");
        var sizeX = (float)_hpBase.Call("GetSizeX");
        var sizeY = (float)_hpBase.Call("GetSizeY");
        Position = new Vector2(x -(sizeX/2),y -(sizeY/2));
    }

    public void SetSizeX()
    {
        var hp = (float)_character.Hp;
        var hpMax = (float)_character.HpMax;
        var size = hp/hpMax;

        Scale = new Vector2(200 * size, Scale.Y);
    }
}
