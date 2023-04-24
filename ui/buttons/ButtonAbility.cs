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
        _control.SetAbilityStats(_whoseTurn.NormalAtk());
    }

    public void Ability1()
    {

    }

    public void Ability2()
    {

    }

    public void Ability3()
    {

    }

    public void Ability4()
    {

    }
}
