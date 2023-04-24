using Godot;
using System;

public partial class Mana : Sprite2D
{
    public string NameClass { get; set; }
    private Character _character;
    private ManaBase _manaBase;
    public override void _Ready()
    {
        _character = GetParent<Character>();
        if (_character.HaveMana == false)
        {
            TouglleVisible();
        }
        _manaBase = GetNode<ManaBase>("../ManaBase");
        GetNameClass();
        SetSizeX();
    }

    public override void _Process(double delta)
    {



    }

    public void TouglleVisible()
    {
        Visible = !Visible;
    }

    public void GetNameClass()
    {
        var parent = GetParent().Name;
        NameClass = _character.NameClass;
    }

    public void SetPosition()
    {
        var x = (float)_manaBase.Call("GetPosX");
        var y = (float)_manaBase.Call("GetPosY");
        var sizeX = (float)_manaBase.Call("GetSizeX");
        var sizeY = (float)_manaBase.Call("GetSizeY");
        Position = new Vector2(x - (sizeX / 2), y - (sizeY / 2));
    }

    public void SetSizeX()
    {
        var mana = (float)_character.Mana;
        var manaMax = (float)_character.ManaMax;
        var size = mana / manaMax;

        Scale = new Vector2(200 * size, Scale.Y);
    }
}
