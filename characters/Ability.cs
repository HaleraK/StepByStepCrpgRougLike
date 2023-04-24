using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
using static Ability;

public partial class Ability : Node
{
        public class Target
        {
            public int PosiblePos { get; set; }
            public int Priority { get; set; }
            public Target(int posiblePos, int priority)
            {
                PosiblePos = posiblePos;
                Priority = priority;
            }
            public Target()
            {

            }
        }
    public Character CharSource { get; set; }
    public string NameAbility { get; set; }
    public bool Learned { get; set; } = false;
    public string RequireToKnow { get; set; } = "";



    private Target[] _targets;
    public int TargetCount { get; set; }
    public int CountActions { get; set; } = 1;
    public bool Selected { get; set; } = false;
    public int CoolDown { get; set; } = 0;
    public bool OnCoolDown { get; set; } = false;

    public float Damage { get; set; } = 0;
    public float Heal { get; set; } = 0;
    public float ManaCost { get; set; } = 0;
    public float BuffDebuffStats { get; set; }
    public string BuffDebuffName { get; set; }

    public string TargetType { get; set; } //Character Mob
    public string Type { get; set; } //Atk Heal AoeAtk AoeHeal
    

    public readonly static string[] POSIBLE_TARGET_TYPE = { "Character", "Mob", "Self", "Any" };
    public readonly static string[] POSIBLE_TYPE = { "Atk", "Heal", "BuffDebuff" };

    public Ability (string type, string targetType)
    {
        Type = type;
        TargetType = targetType;
    }

    public Ability(string nameSkill, string type, string targetType)
    {
        NameAbility = nameSkill;
        Type = type;
        TargetType = targetType;
    }

    public Ability(string nameSkill)
    {
        NameAbility = nameSkill;
    }

    public Ability()
    {
        
    }

    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void AddTarget(int posiblePos, int priority)
    {
        Array.Resize(ref _targets, _targets.Length + 1);
        _targets[_targets.Length - 1] = new Target(posiblePos, priority);
    }
}
