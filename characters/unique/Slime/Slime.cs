using Godot;
using StepByStepCrpgRougLike.characters;
using System;
using System.Linq;
using static Ability;
using static Character;
using static Control;

public partial class Slime : Area2D
{
    private Character _character;
    private Ability _ability;
    private Ability[] _abilitiesClass;
    private string[] chosenAbilities = { "NormalAttack", "NormalAttack", "NormalAttack", "NormalAttack" };

    public override void _Ready()
	{
        _character = GetParent<Character>();
        CreateAbilitiesClass();

    }

	public override void _Process(double delta)
	{
	}

    public void CreateAbilitiesClass()
    {
        Array.Resize(ref _abilitiesClass, 5);

        _abilitiesClass[0] = new Ability("NormalAttack");
        _abilitiesClass[1] = new Ability("TripleAtk");
        _abilitiesClass[2] = new Ability("ManaAtk");
        _abilitiesClass[3] = new Ability("HealAlly");
        _abilitiesClass[4] = new Ability("SelfHeal");
    }

    public Ability CallAbilityByName(string name)
    {
        return (Ability)this.Call(name);
    }

    public Ability GetByNameAbility(string nameAbility)
    {
        return _abilitiesClass.Where(a => a.NameAbility == nameAbility).FirstOrDefault();
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

    public bool IsOnCoolDown(Ability ability)
    {
        if (ability.CoolDown > 0 && ability.OnCoolDown)
        {
            return true;
        }
        return false;
    }

    public void TougleVisible()
    {
        Visible = !Visible;
    }

    public Ability Ability(int n)
    {
        return CallAbilityByName(chosenAbilities[n]);

    }

    public Ability NormalAttack()
    {
        Ability stats = GetByNameAbility("NormalAttack");

        stats.Selected = true;
        stats.Type = "Atk";
        stats.TargetType = "Character";
        stats.SetPriorityOfTargets("Front");
        stats.CharSource = _character;

        stats.TargetCount = 1;
        stats.CountActions = 1;
        stats.CoolDown = 0;

        float dammage = 0;
        dammage += _character.Atk;

        stats.Damage = dammage;

        return stats;
    }
}
