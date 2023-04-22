using Godot;
using System;

public partial class ButtonAbility : Button
{
	[Export] public string Type { get; set; }
	private readonly static string[] POSIBLE_TYPES
		= { "NormalAtk", "Ability", "Ability2", "Ability3", "Ability4" };

    private Control _control;

    public override void _Ready()
	{
        _control = GetNode<Control>("../../Control");
    }

	public override void _Process(double delta)
	{
	}

	public void OnPressed()
	{
        string whoseTurn = (string)_control.WhoseTurn;
        string typeTurn = (string)_control.TypeTurn;

        var node = GetNode("../../").GetNode("ConteinerOfChars/" + whoseTurn);

		if (Type == "NormalAtk" && typeTurn == "Character")
		{
			float damage = (float)node.Call("GiveDamage");
            _control.Set("Damage", damage);
        }
	}

}
