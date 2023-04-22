using Godot;
using System;

public partial class UIController : Node
{
    private InitBase _initBarBase;
    private Init _initBar;
    private HpBase _hpBarBase;
    private Hp _hpBar;
    private ArmorText _armorText;

    private Character _character;

    public override void _Ready()
    {
        _character = GetParent<Character>();
        _character.DamageTaken += ChageHpBarLength;
        _character.InitChanged += ChageInitBarLength;

        _initBarBase = GetNode<InitBase>("../InitBase");
        _initBar = GetNode<Init>("../Init");
        _hpBarBase = GetNode<HpBase>("../HpBase");
        _hpBar = GetNode<Hp>("../Hp");
        _armorText = GetNode<ArmorText>("../ArmorText");
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
        _armorText.SetPosition();
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
    }
}
