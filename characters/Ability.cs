using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
using static Ability;

public partial class Ability : Node
{
        public class Target
        {
            public int Position { get; set; }
            public int Priority { get; set; }
            public Target(int pos, int priority)
            {
                Position = pos;
                Priority = priority;
            }
        public Target(int pos)
        {
            Position = pos;
        }
        public Target()
            {

            }
        }

    public class BuffDebuff 
    {
        public string NameBuff { get; set; } = "";
        public float Value { get; set; } = 0;
        public int CountTurns { get; set; } = 0;

        public BuffDebuff(string nameBuff, float value, int countTurns) 
        {
            NameBuff = nameBuff;
            Value = value;
            CountTurns = countTurns;
        }
        public BuffDebuff() { }
    }

    public Character CharSource { get; set; }
    public BuffDebuff BuffDebuffA { get; set; } = new();
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

    public string TargetType { get; set; } //Character Mob
    public string Type { get; set; } //Atk Heal AoeAtk AoeHeal
    

    public readonly static string[] POSIBLE_TARGET_TYPE = { "Character", "Mob", "Self", "Any" };
    public readonly static string[] POSIBLE_TYPE = { "Atk", "Heal", "BuffDebuff" };
    public readonly static string[] POSIBLE_TYPETARGETS = { "Front", "Mid", "End", "Any", "FrontAoe", "MidAoe", "EndAoe", "Aoe" };

    public Ability (string type, string targetType)
    {
        Type = type;
        TargetType = targetType;
        CreateTargets();
    }

    public Ability(string nameSkill, string type, string targetType)
    {
        NameAbility = nameSkill;
        Type = type;
        TargetType = targetType;
        CreateTargets();
    }

    public Ability(string nameSkill)
    {
        NameAbility = nameSkill;
        CreateTargets();
    }

    public Ability()
    {
        CreateTargets();
    }

    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private void CreateTargets()
    {
        Array.Resize(ref _targets, 6);
        for (int i =0; i < _targets.Length; i++) 
        {
            _targets[i] = new(i+1);
        }
    }

    public Target[] GetTargets() 
    { 
        return _targets;
    }

    public void SetPriorityOfTargets(string type)
    {
        switch (type)
        {
            case ("Front"):
                GivePriorityOfTargets(1, 2, 3);
                break;
            case ("FrontMid"):
                GivePriorityOfTargets(1, 1, 2);
                break;
            case ("FrontEnd"):
                GivePriorityOfTargets(1, 2, 1);
                break;
            case ("Mid"):
                GivePriorityOfTargets(3, 1, 2);
                break;
            case ("MidEnd"):
                GivePriorityOfTargets(2, 1, 1);
                break;
            case ("End"):
                GivePriorityOfTargets(3, 2, 1);
                break;
            case ("Any"):
                GivePriorityOfTargets(1, 1, 1);
                break;
        }
    }

    private void GivePriorityOfTargets(int prior1, int prior2, int prior3)
    {
        _targets[0].Priority = prior1;
        _targets[1].Priority = prior1;
        _targets[2].Priority = prior2;
        _targets[3].Priority = prior2;
        _targets[4].Priority = prior3;
        _targets[5].Priority = prior3;
    }
}
