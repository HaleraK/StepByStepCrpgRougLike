using Godot;
using System;

public partial class ButtonAbility : Button
{
	[Export] public string Type { get; set; }
    public string TypeTurn { get; set; }
    private readonly static string[] POSIBLE_TYPES
		= { "NormalAtk", "Ability1", "Ability2", "Ability3", "Ability4" };

    private Control _control;
    private Character _whoseTurn;

    private string _path = "res://characters/unique/";

    public override void _Ready()
	{
        _control = GetNode<Control>("../../Control");
    }

	public override void _Process(double delta)
	{
	}

    public void SetWhoseTurn(Character whoseTurn, string type)
    {
        _whoseTurn = whoseTurn;
        TypeTurn = type;
    }

    public void DelWhoseTurn()
    {
        _whoseTurn = null;
        TypeTurn = "";
    }

    public void SetSkin(string nameClass, string abilityName)
    {
        switch (Type)
        {
            case "NormalAtk":
                Icon = (Texture2D)ResourceLoader.Load(_path + nameClass + "/" + abilityName + ".png");
                break;
            case "Ability1":
                Icon = (Texture2D)ResourceLoader.Load(_path + nameClass + "/" + abilityName + ".png");
                break;
            case "Ability2":
                Icon = (Texture2D)ResourceLoader.Load(_path + nameClass + "/" + abilityName + ".png");
                break;
            case "Ability3":
                Icon = (Texture2D)ResourceLoader.Load(_path + nameClass + "/" + abilityName + ".png");
                break;
            case "Ability4":
                Icon = (Texture2D)ResourceLoader.Load(_path + nameClass + "/" + abilityName + ".png");
                break;
        }
    }


    public void OnPressed()
	{
        if (TypeTurn == "Character")
        {
            switch (Type)
            {
                case "NormalAtk":
                    NormalAtk();
                    break;
                case "Ability1":
                    Ability1();
                    break;
                case "Ability2":
                    Ability2();
                    break;
                case "Ability3":
                    Ability3();
                    break;
                case "Ability4":
                    Ability4();
                    break;
            }
        }
	}

	public void NormalAtk() 
	{
        _control.Ability = _control.WhoseTurn.NormalAtk();
        if ("Self" == _control.Ability.TargetType)
        {
            _control.WhoseTurn.InputAbility();
        }
    }

    public void Ability1()
    {
        _control.Ability = _control.WhoseTurn.Ability1();
        if ("Self" == _control.Ability.TargetType)
        {
            _control.WhoseTurn.InputAbility();
        }
    }

    public void Ability2()
    {
        _control.Ability = _control.WhoseTurn.Ability2();
        if ("Self" == _control.Ability.TargetType)
        {
            _control.WhoseTurn.InputAbility();
        }

    }

    public void Ability3()
    {
        _control.Ability = _control.WhoseTurn.Ability3();
        if ("Self" == _control.Ability.TargetType)
        {
            _control.WhoseTurn.InputAbility();
        }

    }

    public void Ability4()
    {
        _control.Ability = _control.WhoseTurn.Ability4();
        if ("Self" == _control.Ability.TargetType)
        {
            _control.WhoseTurn.InputAbility();
        }

    }
}
