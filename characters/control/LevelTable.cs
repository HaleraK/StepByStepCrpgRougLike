using Godot;
using System;

public partial class LevelTable : Node
{
    public class Level
    {
        public float Hp { get; set; } 
        public float Mana { get; set; } 
        public float Armor { get; set; } 
        public float Atk { get; set; } 
        public float InitiativeSpeed { get; set; } 
        public float CritRate { get; set; } 
        public float CritAtk { get; set; }
        public int LVL { get; set; } 

        public int PointsForActive { get; set; }
        public int PointsForPassive { get; set; }
        public int PointsForStats { get; set; }

        public float NeedExpForNextLVL { get; set; }
        public Level()
        {
        }

        public Level(int lVL) 
        { 
            LVL = lVL;
        }
        public Level(float hp, float mana, float armor, float atk, float initiative, float critRate, float critAtk, int lVL, int pointsForActive, int pointsForPassive, int pointsForStats, float needExpForNextLVL)
        {
            Hp = hp;
            Mana = mana;
            Armor = armor;
            Atk = atk;
            InitiativeSpeed = initiative;
            CritRate = critRate;
            CritAtk = critAtk;
            LVL = lVL;
            PointsForActive = pointsForActive;
            PointsForPassive = pointsForPassive;
            PointsForStats = pointsForStats;
            NeedExpForNextLVL = needExpForNextLVL;
        }
    }

    public const int LVLMAX = 10;
    public const float CRIT_RATE = 5;
    public const float CRIT_ATK = 50;
    public const float ARMOR = 10;
    public const float INIT_SPEED = 10;
    public const int ONE_POINT = 1;

    //уровни от 0 до 10
    private Level[] _level;

    //универсальные таблица
    //разнообразие за счет _pointsForStats
    public override void _Ready()
	{
        CreateTableAndFieldIt();
        ConsolOut();



    }

	public override void _Process(double delta)
	{
        
	}

    public void CreateTableAndFieldIt()
    {
        Array.Resize(ref _level, LVLMAX + 1);

        float hp = 100;
        float mana = 100;
        float xp = 100;

        float atk = 10;

        for (int i = 0; i < _level.Length; i++)
        {
            _level[i] = new(i);

            _level[i].Hp = hp;
            _level[i].Mana = mana;
            _level[i].NeedExpForNextLVL = xp;
            hp += MathF.Ceiling(hp/2) + 50;
            mana += MathF.Ceiling(mana / 4) + 25;
            xp *= 2;

            _level[i].Atk = atk;
            atk += MathF.Ceiling(atk / 3) + 5;

            _level[i].Armor = ARMOR;
            _level[i].InitiativeSpeed = INIT_SPEED;
            _level[i].CritRate = CRIT_RATE;
            _level[i].CritAtk = CRIT_ATK;

            _level[i].PointsForActive = ONE_POINT;
            _level[i].PointsForPassive = ONE_POINT;
            _level[i].PointsForStats = ONE_POINT * 5;

            
        }
    }

    public void ConsolOut()
    {
        GD.Print("LVL\t\t\t" +
                     "Exp\t\t\t" +
                     "Hp\t\t\t" +
                     "Atk\t\t\t" +
                     "Mana\t\t\t" +
                     "Armor\t\t\t" +
                     "InitSpeed\t\t\t" +
                     "CritRate\t\t\t" +
                     "CritAtk\t\t\t" +
                     "PointsA\t\t" +
                     "PointsR\t\t" +
                     "PointsS\t\t");
        for (int i = 0; i < _level.Length; i++)
        {
            GD.Print($"{_level[i].LVL}\t\t\t" +
                     $"{_level[i].NeedExpForNextLVL}\t\t\t" +
                     $"{_level[i].Hp}\t\t\t" +
                     $"{_level[i].Atk}\t\t\t" +
                     $"{_level[i].Mana}\t\t\t" +
                     $"{_level[i].Armor}\t\t\t" +
                     $"{_level[i].InitiativeSpeed}\t\t\t" +
                     $"{_level[i].CritRate}\t\t\t" +
                     $"{_level[i].CritAtk}\t\t\t" +
                     $"{_level[i].PointsForActive}\t\t" +
                     $"{_level[i].PointsForPassive}\t\t" +
                     $"{_level[i].PointsForStats}\t\t");
        }
    }
}
