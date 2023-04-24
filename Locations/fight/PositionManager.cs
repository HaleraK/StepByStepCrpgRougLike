using Godot;
using System;

public partial class PositionManager : Node
{
    private Point[] _pointChars;
    private Point[] _pointMobs;
    private ButtonAbility[] _buttonsAbilities;
    private Character[] _allChars;

    public override void _Ready()
    {
        GetCharactersList();

        GetPointCharactersList();
        GetPointMobsList();
        GetButtonsAbilitiesList();

        SetPositionsPointCharacters();
        SetPositionsPointMobs();
        SetPositionsButtonsAbilities();

        SetPositionChars();
    }

    public override void _Process(double delta)
    {
    }

    public void SetPositionChars()
    {
        int posNumber;

        Vector2 pos;
        foreach (var character in _allChars)
        {
            if (character.Type == "Character")
            {
                posNumber = character.PositionMarker;
                pos = _pointChars[posNumber - 1].Position;
                character.Position = new Vector2(pos.X, pos.Y - (character.GetSizeY() / 2));
                GD.Print(character.GetSizeY());

            }
            else if (character.Type == "Mob")
            {
                posNumber = character.PositionMarker;
                pos = _pointMobs[posNumber - 1].Position;
                character.Position = new Vector2(pos.X, pos.Y - (character.GetSizeY() / 2));
            }
        }
    }

    public void GetCharactersList()
    {
        Array.Resize(ref _allChars, 0);
        var node = GetNode("../ConteinerOfChars");
        var children = node.GetChildren();

        var i = 0;
        Array.Resize(ref _allChars, children.Count);
        foreach (var child in children)
        {
            _allChars[i] = GetNode<Character>("../ConteinerOfChars/" + child.Name);
            i++;
        }
    }

    public void GetPointCharactersList()
    {
        Array.Resize(ref _pointChars, 0);
        var node = GetNode("../ConteinerPointChar");
        var children = node.GetChildren();

        var i = 0;
        Array.Resize(ref _pointChars, children.Count);
        foreach (var child in children)
        {
            _pointChars[i] = GetNode<Point>("../ConteinerPointChar/" + child.Name);
            i++;
        }
    }

    public void GetPointMobsList()
    {
        Array.Resize(ref _pointMobs, 0);
        var node = GetNode("../ConteinerPointMob");
        var children = node.GetChildren();

        var i = 0;
        Array.Resize(ref _pointMobs, children.Count);
        foreach (var child in children)
        {
            _pointMobs[i] = GetNode<Point>("../ConteinerPointMob/" + child.Name);
            i++;
        }
    }

    public void GetButtonsAbilitiesList()
    {
        Array.Resize(ref _buttonsAbilities, 0);
        var node = GetNode("../ContainerOfActionButtons");
        var children = node.GetChildren();

        var i = 0;
        Array.Resize(ref _buttonsAbilities, children.Count);
        foreach (var child in children)
        {
            _buttonsAbilities[i] = GetNode<ButtonAbility>("../ContainerOfActionButtons/" + child.Name);
            i++;
        }
    }

    public void SetPositionsButtonsAbilities()
    {
        float screenSizeX = GetViewport().GetVisibleRect().Size.X;
        float screenSizeY = GetViewport().GetVisibleRect().Size.Y;

        screenSizeY -= 50;
        screenSizeY -= (_buttonsAbilities[0].Size.Y * (float)0.08);
        screenSizeX -= 40;


        foreach (var button in _buttonsAbilities)
        {
            screenSizeX -= 10;
            screenSizeX -= (button.Size.X * (float)0.08);
            button.Position = new Vector2(screenSizeX, screenSizeY);
        }
    }

    public void SetPositionsPointCharacters()
    {
        float screenSizeX = GetViewport().GetVisibleRect().Size.X;
        float screenSizeY = GetViewport().GetVisibleRect().Size.Y;
        float screenSizeY2 = screenSizeY;

        screenSizeX -= (screenSizeX / 2);
        screenSizeX -= 200;

        var tmp = screenSizeY;

        screenSizeY = (tmp / (float)(1.9));
        screenSizeY2 = (tmp / (float)(1.3));

        float step = screenSizeX / (float)3;

        for (int i = 0; i < MathF.Round(_pointChars.Length / 2); i++)
        {
            _pointChars[i * 2].Position = new Vector2(screenSizeX, screenSizeY);
            _pointChars[(i * 2) + 1].Position = new Vector2(screenSizeX - 100, screenSizeY2);
            screenSizeX -= step;
        }
    }

    public void SetPositionsPointMobs()
    {
        float screenSizeX = GetViewport().GetVisibleRect().Size.X;
        float screenSizeY = GetViewport().GetVisibleRect().Size.Y;
        float screenSizeY2 = screenSizeY;

        screenSizeX -= (screenSizeX / 2);
        screenSizeX += 200;

        var tmp = screenSizeY;

        screenSizeY = (tmp / (float)(1.9));
        screenSizeY2 = (tmp / (float)(1.3));

        var tmp2 = GetViewport().GetVisibleRect().Size.X - (float)(GetViewport().GetVisibleRect().Size.X / 2);
        tmp2 -= 200;
        float step = tmp2 / (float)3;

        for (int i = 0; i < MathF.Round(_pointMobs.Length / 2); i++)
        {
            _pointMobs[i * 2].Position = new Vector2(screenSizeX, screenSizeY);
            _pointMobs[(i * 2) + 1].Position = new Vector2(screenSizeX + 100, screenSizeY2);
            screenSizeX += step;
        }

    }
}
