using Godot;
using System;
using System.Linq;
using System.Xml.Linq;
using static Ability;
using static Character;
using static Control;

public partial class Warrior : Area2D
{
    private Character _character;
    private Ability _ability;
    private Ability[] _abilitiesClass;
    private string[] chosenAbilities = { "ShieldAtk", "ShielThrowdAtk", "HammerThrowdAtk", "ArmorBuff" };

    public override void _Ready()
	{
        _character = GetParent<Character>();
        CreateAbilitiesClass();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void CreateAbilitiesClass()
    {
        Array.Resize(ref _abilitiesClass, 5);

        _abilitiesClass[0] = new Ability("NormalAttack");
        _abilitiesClass[1] = new Ability("ShieldAtk");
        _abilitiesClass[2] = new Ability("ShielThrowdAtk");
        _abilitiesClass[3] = new Ability("HammerThrowdAtk");
        _abilitiesClass[4] = new Ability("ArmorBuff");
    }

    public Ability CallAbilityByName(string name)
    {
        return (Ability)this.Call(name);
    }

    public void TougleVisible()
    {
        Visible = !Visible;
    }

    public void DecrementCoolDowns()
    {
        foreach (var ability in _abilitiesClass)
        {
            if (ability.CoolDown > 0)
            {
                ability.CoolDown--;
            }
            else
            {
                ability.OnCoolDown = false;
            }
        }
    }

    public void DecrementBuffDeburrCountTurns()
    {
        foreach (var ability in _abilitiesClass)
        {
            if (ability.BuffDebuffA.CountTurns > 0)
            {
                ability.BuffDebuffA.CountTurns--;
            }
            else
            {
                UnBuffDebuff(ability);
            }
        }
    }

    public void UnBuffDebuff(Ability ability)
    {
        BuffDebuffList.UnBuffDebuffDetector.Invoke(ability.BuffDebuffA.NameBuff, ability.BuffDebuffA.Value, _character);
        ability.BuffDebuffA = new(); 
    }

    public Ability GetByNameAbility(string nameAbility)
    {
        return _abilitiesClass.Where(a => a.NameAbility == nameAbility).FirstOrDefault();
    }

    public bool IsOnCoolDown(Ability ability)
    {
        if (ability.CoolDown > 0 && ability.OnCoolDown)
        {
            return true;
        }
        return false;
    }

    public Ability Ability(int n)
    {
        return CallAbilityByName(chosenAbilities[n]);

    }

    //
    //обычная атака 80% атаки, но баф защиты на 30%
    public Ability NormalAttack()
    {
        Ability stats = GetByNameAbility("NormalAttack");
        GetByNameAbility("NormalAttack").BuffDebuffA = new Ability.BuffDebuff("ArmorIncrease", (float)1.30, 2);

        stats.Selected = true;
        stats.Type = "Atk";
        stats.TargetType = "Mob";
        stats.SetPriorityOfTargets("Front");

        stats.CharSource = _character;
        stats.TargetCount = 1;
        stats.CountActions = 1;
        stats.CoolDown = 0;

        float dammage = 0;
        dammage += _character.Atk;
        dammage *= (float)0.8;

        stats.Damage = dammage;

        return stats;
    }

    //
    //атака щитом 80% атаки + армор/2
    public Ability ShieldAtk()
    {
        Ability stats = GetByNameAbility("ShieldAtk");

        stats.Selected = true;
        stats.Type = "Atk";
        stats.TargetType = "Mob";
        stats.SetPriorityOfTargets("Front");

        stats.CharSource = _character;
        stats.TargetCount = 1;
        stats.CountActions = 1;
        stats.CoolDown = 0;

        float dammage = 0;
        dammage += _character.Atk;
        dammage *= (float)0.8;
        dammage += (_character.Armor/2);

        stats.Damage = dammage;

        return stats;
    }

    //
    //усилинная атака щитом 80% атаки + армор*2
    public Ability ShielThrowdAtk()
    {
        Ability stats = GetByNameAbility("ShielThrowdAtk");

        stats.Selected = true;
        stats.Type = "Atk";
        stats.TargetType = "Mob";
        stats.SetPriorityOfTargets("Any");

        stats.CharSource = _character;
        stats.TargetCount = 1;
        stats.CountActions = 1;
        stats.CoolDown = 3;

        float dammage = 0;
        dammage += _character.Atk;
        dammage *= (float)0.8;
        dammage += (_character.Armor * 2);

        stats.Damage = dammage;

        return stats;
    }

    //
    //усилинная атака на 20%
    public Ability HammerThrowdAtk()
    {
        Ability stats = GetByNameAbility("HammerThrowdAtk");
        if (IsOnCoolDown(GetByNameAbility("HammerThrowdAtk")))
        {
            return stats;
        }
        GetByNameAbility("HammerThrowdAtk").CoolDown = 3;

        stats.Selected = true;
        stats.Type = "Atk";
        stats.TargetType = "Mob";
        stats.SetPriorityOfTargets("Any");

        stats.CharSource = _character;
        stats.TargetCount = 1;
        stats.CountActions = 1;
        stats.CoolDown = 3;

        float dammage = 0;
        dammage += _character.Atk;
        dammage *= (float)1.2;

        stats.Damage = dammage;

        return stats;
    }

    //
    //баф на защиту
    public Ability ArmorBuff()
    {
        Ability stats = GetByNameAbility("ArmorBuff");
        GetByNameAbility("ArmorBuff").BuffDebuffA = new Ability.BuffDebuff("ArmorIncrease", (float)2, 3);

        if (IsOnCoolDown(GetByNameAbility("ArmorBuff")))
        {
            return stats;
        }
        GetByNameAbility("ArmorBuff").CoolDown = 5;

        stats.Selected = true;
        stats.Type = "BuffDebuff";
        stats.TargetType = "Self";

        stats.CharSource = _character;
        stats.TargetCount = 1;
        stats.CountActions = 1;
        stats.CoolDown = 5;

        float dammage = 0;
        dammage += _character.Atk;
        dammage *= (float)1.2;

        stats.Damage = dammage;

        return stats;
    }

}
