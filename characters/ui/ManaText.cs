using Godot;
using System;

public partial class ManaText : Label
{
    private ManaBase _manaBase;
    private Character _character;
    public override void _Ready()
    {
        _character = GetParent<Character>();
        if (_character.HaveMana == false)
        {
            TouglleVisible();
        }
        _manaBase = GetNode<ManaBase>("../ManaBase");
        SetText();
        SetPosition();
    }

    public override void _Process(double delta)
    {
    }

    public void TouglleVisible()
    {
        Visible = !Visible;
    }

    public void SetText()
    {
        Text = _character.Mana.ToString("R");
        Text += "/";
        Text += _character.ManaMax.ToString("R");
    }

    public void SetPosition()
    {
        var x = (float)_manaBase.Call("GetPosX");
        var y = (float)_manaBase.Call("GetPosY");
        Position = new Vector2(x - Size.X/2, y - 15);

    }
}
