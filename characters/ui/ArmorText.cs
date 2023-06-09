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
        SetText();
		SetPosition();
    }

	public override void _Process(double delta)
	{
    }

	public void SetText()
	{
        Text = _character.Armor.ToString("R");
	}

    public void SetPosition()
    {
        var x = (float)_hpBase.Call("GetPosX");
        var y = (float)_hpBase.Call("GetPosY");
        var sizeX = (float)_hpBase.Call("GetSizeX");
        Position = new Vector2(+ x + (sizeX / 2) + 2, y - 15);

    }
}
