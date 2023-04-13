using Godot;
using System;

public partial class Character : Sprite2D
{
	[Export] private int _hp = 100;
    [Export] private int _hpMax = 100; //имеет смысл сделать массив с
									   //максимальными значениями зависимым от уровня

    [Export] private int _mana = 100;
	[Export] private int _manaMax = 100;

	[Export] private int _armor = 10;
	[Export] private int _armorMax = 10;

	[Export] private int _atk = 10;
    [Export] private int _atkMax = 10;

    [Export] private int _initiative = 10;
	[Export] private int _critRate = 0;
	[Export] private int _critAtk = 140;
	[Export] private int _lvl = 1;

    private const int LVLMAX = 10;

    private int _expirence = 0;
	private int[] _countExpForLVL;

	private string _slots;
    // Called when the node enters the scene tree for the first time.
	public void TakeDamage(int gamage)
	{

	}

	public int GiveDamage()
	{
		int damage = 0;
		return damage;

	}
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
