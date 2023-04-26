using Godot;
using StepByStepCrpgRougLike.characters;
using System;

public partial class Character : Node2D
{
    private string _id;
    private CharacterStats _stats;

    [Export] public int PositionMarker { get; set; } = 1;

    [Export] public float Hp { get; set; } = 80;
    [Export] public float HpRegen { get; set; } = 10;
    [Export] public float HpMax { get; set; }  = 100; //имеет смысл сделать массив с
                                                      //максимальными значениями зависимым от уровня
    [Export] public float Mana { get; set; }  = 100;
    [Export] public float ManaRegen { get; set; }  = 10;
    [Export] public float ManaMax { get; set; }  = 100;

    [Export] public float Armor { get; set; }  = 10;
    [Export] public float ArmorMax { get; set; } = 10;

    [Export] public float Atk { get; set; } = 30;
    [Export] public float AtkMax { get; set; } = 10;

    [Export] public float Initiative { get; set; } = 8;
    public float InitiativeCurrent { get; set; } = 0;
    [Export] public float InitiativeMax { get; set; } = 100;

    [Export] public float CritRate { get; set; } = 5;
    [Export] public float CritAtk { get; set; } = 50;

    [Export] public int LVL { get; set; } = 1;
    public bool Alive = true;
    [Export] public bool HaveMana = true;
    

    [Export] public string Type { get; set; } = "";
    private static readonly string[] POSIBLE_TYPES = { "Character", "Mob", "Npc" };

    [Export] public string NameClass { get; set; } = "";
    private static readonly string[] POSIBLE_NAMES = { "Paladin", "Slime", "Warrior"};

    private string[] _charUiNames = { "Hp", "HpBase", "Init", "InitBase", "ArmorText" };

    public int PointsForActive { get; set; }
    public int PointsForPassive { get; set; }
    public int PointsForStats { get; set; }

    private const int LVLMAX = 10;

    public int Exp { get; set; } = 0;
    private int[] _countExpForLVL;

    private string _slots;
    public bool PauseForAction { get; set; }

    private float _time = 0;
    private string _color;

    private UIController _uiController;
    private Control _control;

    public Action<float> DamageTaken { get; set; }
    public Action<float> InitChanged { get; set; }
    public Action<float> ManaChanged { get; set; }
    public Action<float> ArmorChanged { get; set; }
    public Action<Character> InitChangedTo100 { get; set; }


    public override void _Ready()
    {
        DeleteUnusedClases();
        _uiController = GetNode<UIController>("UIController");
        _control = GetNode<Control>("../../Control");

    }

    public override void _Process(double delta)
    {
        InitiativeCounter(delta);
        SetOutlineMode((float)delta);
        IsAlive();
    }

    public void DeleteUnusedClases()
    {
        foreach (var nameClass in POSIBLE_NAMES)
        {
            if (nameClass != NameClass)
            {
                GetNode(nameClass).QueueFree();
            }
        }
    }

    public void InitiativeCounter(double delta)
    {
        if (PauseForAction == true)
        {
            return;
        }
        InitiativeCurrent += Initiative * (float)delta;


        if (InitiativeCurrent >= InitiativeMax)
        {
            InitiativeCurrent = InitiativeMax;
            PauseForAction = true;
            InitChanged.Invoke(InitiativeCurrent);
            InitChangedTo100.Invoke(this);
            return;
        }
        InitChanged.Invoke(InitiativeCurrent);
    }
    public void SetInitTo0()
    {
        InitiativeCurrent = 0;
        PauseForAction = false;
    }

    public void StartTurn()
    {
        IsAlive();
        RestorManaPerTurn();
        RestorHpPerTurn();

        var charClass = GetNode(NameClass);
        charClass.Call("DecrementCoolDowns");
        charClass.Call("DecrementBuffDeburrCountTurns");

        ArmorChanged.Invoke(Armor);
    }

    public void EndTurn()
    {
        IsAlive();
        SetInitTo0();

    }

    public void RestorManaPerTurn()
    {
        Mana += ManaRegen;
        IsManaBiggerManaMax();
        ManaChanged.Invoke(Mana);
    }

    public void RestorHpPerTurn()
    {
        Hp += HpRegen;
        IsHpBiggerHpMax();
        DamageTaken.Invoke(Hp);
    }

    public void LinkAbilities()
    {

        
    }

    //абилка отдает значения
    //
    //posible targets
    //heal / atk / buff / debuf
    //
    public Ability NormalAtk()
    {
        var charClass = GetNode(NameClass);
        Ability stats = (Ability)charClass.Call("NormalAttack");
        return stats;
    }

    public Ability Ability1()
    {
        var charClass = GetNode(NameClass);
        Ability stats = (Ability)charClass.Call("Ability",0);
        return stats;
    }

    public Ability Ability2()
    {
        var charClass = GetNode(NameClass);
        Ability stats = (Ability)charClass.Call("Ability", 1);
        return stats;
    }

    public Ability Ability3()
    {
        var charClass = GetNode(NameClass);
        Ability stats = (Ability)charClass.Call("Ability", 2);
        return stats;
    }

    public Ability Ability4()
    {
        var charClass = GetNode(NameClass);
        Ability stats = (Ability)charClass.Call("Ability", 3);
        return stats;
    }


    public void BuffDebuff(Ability ability)
    {
        BuffDebuffList.BuffDeBuffDetector.Invoke(ability.BuffDebuffA.NameBuff, ability.BuffDebuffA.Value, ability.CharSource);
        ability.BuffDebuffA = new();
    }

    // статы можно будет улучшать с помощью поинтов
    // можно улучшать hp mana armor atk initiative critRate critAtk
    public void TakeDamage(float damage)
    {
        damage -= Armor;
        if (damage <= 0)
        {
            damage = 0;
        }
        Hp -= MathF.Round(damage);
        DamageTaken.Invoke(Hp);
    }

    public void TakeHeal(float heal)
    {
        Hp += MathF.Round(heal);
        IsHpBiggerHpMax();

        DamageTaken.Invoke(Hp);
    }

    public void TakeMana(float mana)
    {
        Mana += MathF.Round(mana);
        IsManaBiggerManaMax();

        ManaChanged.Invoke(Mana);
    }

    public void IsHpBiggerHpMax()
    {
        if (Hp > HpMax)
        {
            Hp = HpMax;
        }
    }

    public void IsManaBiggerManaMax()
    {
        if (Mana > ManaMax)
        {
            Mana = ManaMax;
        }
    }

    public float GiveDamage()
    {
        float damage = Atk;
        return damage;
    }

    public void ManaChange(float mana)
    {
        Mana += mana;
        ManaChanged.Invoke(Mana);
    }

    public void IsAlive()
    {
        if (Hp <= 0)
        {
            Alive = false;
            Hp = 0;
            DamageTaken.Invoke(Hp);
        }
    }

    public void InputAbility()
    {
        Ability statsTaken = _control.Ability;

        if (!statsTaken.Selected)
        {
            return;
        }

        if (statsTaken == null)
        {
            return;
        }

        BuffDebuff(statsTaken);
        ArmorChanged.Invoke(Armor);

        switch (statsTaken.Type)
        {
            case ("Atk"):
                AtkInput(statsTaken);
                break;
            case ("Heal"):
                HealInput(statsTaken);
                break;
            case ("BuffDebuff"):
                BuffDebuffInput(statsTaken);
                break;
            default:
                _control.EndTurn();
                break;
        }
    }

    public void OnClick(InputEvent ev)
    {
        var btn = ev as InputEventMouseButton;
        if (((btn != null) && ((int)MouseButton.Left == 1)) && _control.Turn)
        {
            InputAbility();
        }
    }

    public void AtkInput(Ability statsTaken)
    {
        statsTaken.CharSource.ManaChange(-statsTaken.ManaCost);
        for (int i = 0; i < statsTaken.CountActions; i++)
        {
            TakeDamage((float)statsTaken.Damage);
        }
        _control.EndTurn();
    }

    public void HealInput(Ability statsTaken)
    {
        statsTaken.CharSource.ManaChange(-statsTaken.ManaCost);
        for (int i = 0; i < statsTaken.CountActions; i++)
        {
            TakeHeal((float)statsTaken.Heal);
        }
        _control.EndTurn();
    }

    public void BuffDebuffInput(Ability statsTaken)
    {
        _control.EndTurn();
    }

    public void CharacterInput(Ability statsTaken)
    {

    }

    public void MobInput(Ability statsTaken)
    {
        
    }

    public void SelfInput()
    {
        InputAbility();
    }

    public void AnyInput(Ability statsTaken)
    {

    }

    public void OnPaladinInputEvent(Viewport viewport, InputEvent ev, int shape_idx)
    {
        OnClick(ev);
    }

    public void OnSlimeInputEvent(Viewport viewport, InputEvent ev, int shape_idx)
    {
        OnClick(ev);
    }

    public void OnMouseEnter()
    {
        OutlineModeGreen();
    }

    public void OnPaladinMouseEntered()
    {
        OnMouseEnter();
    }

    public void OnSlimeMouseEntered()
    {
        OnMouseEnter();
    }

    public void OnMouseExit()
    {
        if (Type == "Character")
        {
            OutlineModeYellow();
        }
        if (Type == "Mob")
        {
            OutlineModeRed();
        }
    }

    public void OnPaladinMouseExited()
    {
        OnMouseExit();
    }

    public void OnSlimeMouseExited()
    {
        OnMouseExit();
    }

    //оулайн желтый, зеленый, красный, моргающий, красно-зереный (мегающий)
    //желтый - союзные
    //красный - вражеские
    //зеленый - ход вашего персонажа
    //желто-зереный - можно выбрать абилкой
    public void SetOutlineMode(float delta)
    {
        if (PauseForAction ==  false)
        {
            if (Type == "Character")
            {
                OutlineModeYellow();
            }
            if (Type == "Mob")
            {
                OutlineModeRed();
            }
        } 

    }

    public void OutlineModeYellow()
    {
        var node = GetNode(NameClass + "/AnimatedSprite2D");
        node.Call("SetOutlineColor", "#b78a19");
    }
    public void OutlineModeRed()
    {
        var node = GetNode(NameClass + "/AnimatedSprite2D");
        node.Call("SetOutlineColor", "#af4428");
    }
    public void OutlineModeGreen()
    {
        var node = GetNode(NameClass + "/AnimatedSprite2D");
        node.Call("SetOutlineColor", "#22853e");
    }
    public void OutlineModeYellowGreen(float delta)
    {
        _time = Timer(delta, _time);
        if (_time > 1f)
        {
            if (_color == "#22853e")
            {
                _color = "#b78a19";
            } else
            {
                _color = "#22853e";
            }
            var node = GetNode(NameClass + "/AnimatedSprite2D");
            node.Call("SetOutlineColor", _color);
            _time = 0;
        }
    }

    float Timer(float delta, float a)
    {
        return a += 1 * delta;
    }

    public float GetSizeY()
    {
        var node = GetNode(NameClass + "/AnimatedSprite2D");
        var y = (float)node.Call("GetSizeY");
        
        return y;
    }
}
