using Godot;
using System;

public partial class LevelTable : Node
{
    [Export] private float _hp = 80;
    [Export] private float _mana = 100;
    [Export] private float _armor = 10;
    [Export] private float _atk = 10;
    [Export] private float _initiative = 10;
    [Export] private float _critRate = 0;
    [Export] private float _critAtk = 0;
    [Export] private int _lvl = 1;

    private int _pointsForActive;
    private int _pointsForPassive;
    private int _pointsForStats;

    private const int LVLMAX = 10;

    private int _exp = 0;
    private int[] _countExpForLVL;
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
