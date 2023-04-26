using Godot;
using System;

public partial class BuffDebuffList : Node
{
    public static Action<string, float, Character> BuffDeBuffDetector { get; set; }
    public static Action<string, float, Character> UnBuffDebuffDetector { get; set; }

    public override void _Ready()
	{
        BuffDeBuffDetector += BuffDebuffByName;
        UnBuffDebuffDetector += UnBuffDebuffByName;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public static void BuffDebuffByName(string nameBuffDebuff, float value, Character _character)
	{
		switch (nameBuffDebuff)
		{
			case ("ArmorIncrease") :
                ArmorIncrease(value, _character);
				break;

        }
	}

    public static void UnBuffDebuffByName(string nameBuffDebuff, float value, Character _character)
    {
        switch (nameBuffDebuff)
        {
            case ("ArmorIncrease"):
                UnArmorIncrease(value, _character);
                break;

        }
    }

    public static void ArmorIncrease(float armorPercent, Character _character) 
	{
		_character.Armor *= armorPercent;
        _character.ArmorMax *= armorPercent;
    }

    public static void UnArmorIncrease(float armorPercent, Character _character)
    {
        _character.Armor /= armorPercent;
        _character.ArmorMax /= armorPercent;
    }
}
