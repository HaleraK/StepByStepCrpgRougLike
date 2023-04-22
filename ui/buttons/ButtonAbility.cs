using Godot;
using System;

public partial class ButtonAbility : Button
{
	[Export] private string _type = "";
	private readonly static string[] POSIBLE_TYPES
		= { "NormalAtk", "Ability", "Ability2", "Ability3", "Ability4" };

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void OnPressed()
	{
        var node = GetNode("../../Control");
        string whoseTurn = (string)node.Get("_whoseTurn");
        string _typeTurn = (string)node.Get("_typeTurn");

        node = GetNode("../../").GetNode("ConteinerOfChars/" + whoseTurn);

		if(_type == "NormalAtk" && _typeTurn == "character")
		{
			float damage = (float)node.Call("GiveDamage");
			
			node = GetNode("../../Control");
            node.Set("Damage", damage);
        }
	
	}

}
