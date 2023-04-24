using Godot;
using StepByStepCrpgRougLike.characters;
using System;

public partial class Slime : Area2D
{
    private Character _character;
    private Ability _abilityStats;
    
    public override void _Ready()
	{
        _character = GetParent<Character>();

    }

	public override void _Process(double delta)
	{
	}

    public void TougleVisible()
    {
        Visible = !Visible;
    }

    public Ability Ability1()
    {
        return NormalAttack();

    }

    public Ability Ability2()
    {
        return NormalAttack();

    }

    public Ability Ability3()
    {
        return NormalAttack();

    }

    public Ability Ability4()
    {
        return NormalAttack();

    }

    public Ability NormalAttack()
    {
        Ability stats = new("Atk", "Character");
        stats.TargetCount = 1;

        /*
        var m = 1;
        for (int i = 0; i < 6; i++)
        {
            stats.AddTarget(i + 1, m);
            if (m % 2 == 1)
            {
                m++;
            }
        }
        */

        float dammage = 0;
        dammage += _character.Atk;

        stats.Damage = dammage;

        return stats;
    }
}
