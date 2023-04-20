using Godot;
using System;

public partial class Character : Node2D
{
    private string _id;

    [Export] private float _hp = 80;
    private float _hpCurrent = 80;
    [Export] private float _hpMax = 100; //имеет смысл сделать массив с
                                       //максимальными значениями зависимым от уровня

    [Export] private float _mana = 100;
    [Export] private float _manaMax = 100;

    [Export] private float _armor = 10;
    [Export] private float _armorMax = 10;

    [Export] private float _atk = 10;
    [Export] private float _atkMax = 10;

    [Export] private float _initiative = 8;
    private float _initiativeCurrent = 0;
    [Export] private float _initiativeMax = 100;
    [Export] private float _critRate = 0;
    [Export] private float _critAtk = 140;
    [Export] private int _lvl = 1;

    [Export] private string _type = "";
    private static readonly string[] POSIBLE_TYPES = { "character", "mob", "npc" };

    [Export] private string _name = "";
    private static readonly string[] POSIBLE_NAMES = { "Paladin", "Slime"};

    private string[] _nodes = { "Hp", "HpBase", "Init", "InitBase", "ArmorText" };

    private int _pointsForActive;
    private int _pointsForPassive;
    private int _pointsForStats;

    private const int LVLMAX = 10;

    private int _exp = 0;
    private int[] _countExpForLVL;

    private string _slots;
    public bool PauseForAction { get; set; }

    public override void _Ready()
    {
        //Position = new Vector2(300, 300);
        UnHideNode();
        SetCharUiPos();
    }

    public override void _Process(double delta)
    {
        Initiative(delta);

        switch (_name)
        {
            case "Paladin":
                PaladinControler();
                break;
            case "Slime":
                SlimeControler();
                break;
        }
    }

    public void SetCharUiPos()
    {
        var node = GetNode("InitBase");
        node.Call("SetPosition");

        GetNode("Init").Call("SetPosition");
        GetNode("HpBase").Call("SetPosition");

        node = GetNode("Hp");
        node.Call("SetPosition");

        GetNode("ArmorText").Call("SetPosition");
    }

    public string GetNodeName() 
    { 
        return Name;
    }

    public void UnHideNode()
    {
        var node = GetNode(_name);
        node.Call("TougleVisible");
    }

    public void Initiative(double delta)
    {
        if (PauseForAction == true) 
        { 
            return; 
        }

        _initiativeCurrent += _initiative * (float)delta;


        if(_initiativeCurrent >= _initiativeMax)
        {
            _initiativeCurrent = 100;
            PauseForAction = true;
            GD.Print(PauseForAction);
            GetNode("Init").Call("SetSizeX");
            return;
        }
        GetNode("Init").Call("SetSizeX");
        //GD.Print(_initiativeCurrent);
    }


    public void TakeDamage(int gamage)
    {

    }

    public int GiveDamage()
    {
        int damage = 0;
        return damage;

    }

    public void PaladinControler()
    {

    }

    public void SlimeControler()
    {

    }

    // статы можно будет улучшать с помощью поинтов
    // можно улучшать hp mana armor atk initiative critRate critAtk
}
