using Godot;
using System;

public partial class ArmorText : Label
{
    private HpBase _hpBase;
    private Character _character;
    public override void _Ready()
	{
        _character = GetParent<Character>();
        _hpBase = GetNode<HpBase>("../HpBase");
        SetArmor();
		SetPosition();
    }

	public override void _Process(double delta)
	{
    }

	public void SetArmor()
	{
        Text = _character.Armor.ToString("R");
	}

    public void SetPosition()
    {
        var x = (float)_hpBase.Call("GetPosX");
        var y = (float)_hpBase.Call("GetPosY");
        var sizeX = (float)_hpBase.Call("GetSizeX");
        var sizeY = (float)_hpBase.Call("GetSizeY");
        Position = new Vector2(x + 10 + (sizeX / 2), y - 15);

    }
}
