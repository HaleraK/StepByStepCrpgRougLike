using Godot;
using System;

public partial class HpText : Label
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
        Text = _character.Hp.ToString("R");
        Text += "/";
        Text += _character.HpMax.ToString("R");
    }

    public void SetPosition()
    {
        var x = (float)_hpBase.Call("GetPosX");
        var y = (float)_hpBase.Call("GetPosY");
        Position = new Vector2(x - Size.X / 2, y - 15);

    }
}
