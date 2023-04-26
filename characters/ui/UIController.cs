using Godot;
using System;

public partial class UIController : Node
{
    private InitBase _initBarBase;
    private Init _initBar;
    private HpBase _hpBarBase;
    private Hp _hpBar;
    private ManaBase _manaBarBase;
    private Mana _manaBar;
    private ArmorText _armorText;
    private ManaText _manaText;
    private HpText _hpText;

    private Character _character;

    public override void _Ready()
    {
        _character = GetParent<Character>();
        _character.DamageTaken += ChageHpBarLength;
        _character.InitChanged += ChageInitBarLength;
        _character.ManaChanged += ChageManaBarLength;
        _character.ArmorChanged += ChageArmorText;

        _initBarBase = GetNode<InitBase>("../InitBase");
        _initBar = GetNode<Init>("../Init");
        _hpBarBase = GetNode<HpBase>("../HpBase");
        _hpBar = GetNode<Hp>("../Hp");
        _manaBarBase = GetNode<ManaBase>("../ManaBase");
        _manaBar = GetNode<Mana>("../Mana");
        _armorText = GetNode<ArmorText>("../ArmorText");
        _manaText = GetNode<ManaText>("../ManaText");
        _hpText = GetNode<HpText>("../HpText");

        _initBar = GetNode<Init>("../Init");
        _hpBarBase = GetNode<HpBase>("../HpBase");
        _hpBar = GetNode<Hp>("../Hp");
        _manaBarBase = GetNode<ManaBase>("../ManaBase");
        _manaBar = GetNode<Mana>("../Mana");
        _armorText = GetNode<ArmorText>("../ArmorText");
        _manaText = GetNode<ManaText>("../ManaText");
        _hpText = GetNode<HpText>("../HpText");

        UnHideNode();
        SetCharUiPos();
    }

    public override void _Process(double delta)
    {
        
    }

    public void SetCharUiPos()
    {
        _initBarBase.SetPosition();
        _initBar.SetPosition();
        _hpBarBase.SetPosition();
        _hpBar.SetPosition();
        _manaBarBase.SetPosition();
        _manaBar.SetPosition();
        _armorText.SetPosition();
        _manaText.SetPosition();
        _hpText.SetPosition();
    }

    public void UnHideNode()
    {
        var node = GetNode("../").GetNode(_character.NameClass);
        node.Call("TougleVisible");
    }

    public void ChageInitBarLength(float currentInit)
    {      
        _initBar.SetSizeX();
    }
    public void ChageHpBarLength(float damage)
    {
        _hpBar.SetSizeX();
        _hpText.SetText();
    }

    public void ChageManaBarLength(float mana)
    {
        _manaBar.SetSizeX();
        _manaText.SetText();
    }
    public void ChageArmorText(float armor)
    {
        _armorText.SetText();
        GD.Print(_character.Armor);
    }

}
