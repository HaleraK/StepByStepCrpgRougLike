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

    [Export] private float _atk = 30;
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

    private float _time = 0;
    private string _color;

    public override void _Ready()
    {
        //Position = new Vector2(300, 300);
        UnHideNode();
        SetCharUiPos();
    }

    public override void _Process(double delta)
    {
        Initiative(delta);
        SetOutlineMode((float)delta);

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

    public void OnEnemyClick(InputEvent ev)
    {
        var btn = ev as InputEventMouseButton;
        if (((btn != null) && ((int)MouseButton.Left == 1)))
        {
            var node = GetNode("../../Control");
            float damage = (float)node.Get("Damage");
            TakeDamage(-damage);
            node.Call("EndTurn");
        }
    }

    public void OnPaladinInputEvent(Viewport viewport, InputEvent ev, int shape_idx)
    {
        OnEnemyClick(ev);
    }

    public void OnSlimeInputEvent(Viewport viewport, InputEvent ev, int shape_idx)
    {
        OnEnemyClick(ev);
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
        if (_type == "character")
        {
            OutlineModeYellow();
        }
        if (_type == "mob")
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

    public void SetCharUiPos()
    {
        GetNode("InitBase").Call("SetPosition");
        GetNode("Init").Call("SetPosition");
        GetNode("HpBase").Call("SetPosition");
        GetNode("Hp").Call("SetPosition");
        GetNode("ArmorText").Call("SetPosition");
    }

    public string GetNodeName() 
    { 
        return Name;
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
            if (_type == "character")
            {
                OutlineModeYellow();
            }
            if (_type == "mob")
            {
                OutlineModeRed();
            }
        } 

    }

    public void OutlineModeYellow()
    {
        var node = GetNode(_name + "/AnimatedSprite2D");
        node.Call("SetOutlineColor", "#b78a19");
    }
    public void OutlineModeRed()
    {
        var node = GetNode(_name + "/AnimatedSprite2D");
        node.Call("SetOutlineColor", "#af4428");
    }
    public void OutlineModeGreen()
    {
        var node = GetNode(_name + "/AnimatedSprite2D");
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
            var node = GetNode(_name + "/AnimatedSprite2D");
            node.Call("SetOutlineColor", _color);
            _time = 0;
        }
    }

    float Timer(float delta, float a)
    {
        return a += 1 * delta;
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
            GetNode("Init").Call("SetSizeX");
            return;
        }
        GetNode("Init").Call("SetSizeX");
    }


    public void TakeDamage(float damage)
    {
        _hp += damage;
        GetNode("Hp").Call("SetSizeX");
    }

    public float GiveDamage()
    {
        float damage = _atk;
        GD.Print(damage);
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
