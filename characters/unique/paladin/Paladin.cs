using Godot;
using StepByStepCrpgRougLike.characters;
using System;
using static Character;

public partial class Paladin : Area2D
{
	private TreeSkills[] _actives = new TreeSkills[14];
	private TreeSkills[] _passives = new TreeSkills[9];
	private TreeSkills[] _auras = new TreeSkills[4];

	private LevelTable[] _lvlTable = new LevelTable[11];

	private string _currentAura;

	private Character _character;

	public Paladin()
	{
		
	}

    class Slot
	{
		private string _type;
		private static readonly string[] POSIBLE_TYPES 
			= {"head", "body"};
		private string _name;
		private const int MAX_ARTS = 8;

	}

	class TreeSkills
	{
		private string _name;
		private bool _learned = false;
		private string _requiered;
		private string _description;

		public TreeSkills(string name, bool learned, string requiered)
		{
			_name = name;
			_learned = learned;
			_requiered = requiered;
		}
		public TreeSkills(string name, bool learned)
		{
			_name = name;
			_learned = learned;
			_requiered = "";
		}
	}

	class LevelTable
	{
		private int _lvl;
		private int _exp;
		private int _pointsForActive;
		private int _pointsForPassive;
		private int _pointsForStats;

		public LevelTable(int lvl, int exp, int pointsForActive, int pointsForPassive, int pointsForStats)
		{
			_lvl = lvl;
			_exp = exp;
			_pointsForActive = pointsForActive;
			_pointsForPassive = pointsForPassive;
			// статы можно будет улучшать с помощью поинтов
			// можно улучшать hp mana armor atk initiative critRate critAtk
			_pointsForStats = pointsForStats;
		}
	}

	public override void _Ready()
	{

        _actives[0] = new TreeSkills("NormalAttack", true);

		_actives[1] = new TreeSkills("TripleJutice", false, "NormalAttack");
		_actives[2] = new TreeSkills("DivineIntervention", false, "TripleJutice");
		_actives[3] = new TreeSkills("KnightAttack", false, "DivineIntervention");

		_actives[4] = new TreeSkills("HealLight", false, "NormalAttack");
		_actives[5] = new TreeSkills("ResistanceSpirit", false, "HealLight");
		_actives[6] = new TreeSkills("Recovery", false, "ResistanceSpirit");

		_actives[7] = new TreeSkills("DivineDefense", false, "NormalAttack");
		_actives[8] = new TreeSkills("DivineShield", false, "DivineDefense");
		_actives[9] = new TreeSkills("DefenceKnight", false, "DivineShield");

		_actives[10] = new TreeSkills("Entreaty", false, "NormalAttack");
		_actives[11] = new TreeSkills("Challenge", false, "NormalAttack");

		_actives[12] = new TreeSkills("ChangeAura", false, "NormalAttack");
		_actives[13] = new TreeSkills("StrengtheningAura", false, "ChangeAura");


		_passives[0] = new TreeSkills("IncreaseArmor", false);
		_passives[1] = new TreeSkills("IncreaseHeal", false);
		_passives[2] = new TreeSkills("IncreaseDamage", false);
		_passives[3] = new TreeSkills("DivinieSpeed", false);

		_passives[4] = new TreeSkills("MagicArmor", false, "IncreaseArmor");
		_passives[5] = new TreeSkills("DivineProtection", false, "MagicArmor");

		_passives[6] = new TreeSkills("HolyHits", false);
		_passives[7] = new TreeSkills("Fearless", false, "DivinieSpeedd");
		_passives[8] = new TreeSkills("LightsPower", false, "IncreaseDamage");


		_auras[0] = new TreeSkills("AuraAttack", false);
		_auras[1] = new TreeSkills("AuraDefence", false);
		_auras[2] = new TreeSkills("AuraHeal", false);
		_auras[3] = new TreeSkills("AuraSpeed", false);

		int m = 100;

		_lvlTable[0] = new LevelTable(0, 0, 0, 0, 0);
		_lvlTable[1] = new LevelTable(1, m, 1, 0, 1);
		_lvlTable[2] = new LevelTable(2, m * 3, 1, 1, 1);
		_lvlTable[3] = new LevelTable(3, m * 3, 1, 0, 1);
		_lvlTable[4] = new LevelTable(4, m * 3, 1, 0, 2);
		_lvlTable[5] = new LevelTable(5, m * 3, 1, 1, 2);
		_lvlTable[6] = new LevelTable(6, m * 3, 1, 0, 2);
		_lvlTable[7] = new LevelTable(7, m * 3, 1, 0, 3);
		_lvlTable[8] = new LevelTable(8, m * 3, 1, 2, 3);
		_lvlTable[9] = new LevelTable(9, m * 3, 1, 0, 4);
		_lvlTable[10] = new LevelTable(10, m * 3, 1, 2, 6);
	}

	public override void _Process(double delta)
	{
	}

	public void OnMouseEntered()
	{
		
		
	}

    public void TougleVisible()
	{
		Visible = !Visible;
	}

	//АКТИВИ (1 + 13)
	//обычная атака всегда доступна
	//Всего доступно 12 абилок
	//За каждый уровень можно 1 абилку
	//На 10 уровне даються все оставшиися абилки
	//сняражать в бой можно 4 абилки не считая обычной атаки

	//Удар света
	//обычная атака 100% атаки
	public float NormalAttack()
	{
		AbilityStats stats = new();

        float dammage = 0;
		dammage += (float)GetNode(".").Get("Atk");
		return dammage;
	}

	//Тройное правосудие
	//3 атаки со 80% силы
	//откат 3 хода
	public void TripleJutice()
	{

	}

	//Божественное вмешательство
	//Луч света который бьет любую цель 150% силы
	//мана 30%
	public void DivineIntervention()
	{

	}

	//Атака короля
	//Вы кидаете молот по цели, у которой осталось 25% хп
	//мана 20%
	public void KnightAttack()
	{

	}

	//----

	//Лечение света
	//хил любого союзника
	//мана 20%
	public void HealLight()
	{

	}

	//Стойкость духа
	//массовое лечение
	//мана 60%
	//откат 3 хода
	public void ResistanceSpirit()
	{

	}

	//Мольба
	//вы востонавливаете 60% манны и 30% хп
	//откат 4 хода
	public void Entreaty()
	{

	}

	//Востоновление
	//Оживляет недавно падшего союзника с 50% хп
	//мана 60% откат 4 хода
	public void Recovery()
	{

	}

	//----

	//Божественная защита
	//увеличение защиты до 200% до следующего хода
	//мана 20%
	public void DivineDefense()
	{

	}

	//Божественный щит
	//на 1 ход не получаете урон
	//мана 30% откат 3 хода
	public void DivineShield()
	{

	}

	//Защита королей
	//увеличивает защиту до 500% и таргетит врагов на себя на 3 хода
	//мана 60%
	//откат 3 хода
	public void DefenceKnight()
	{

	}

	//----

	//Вызов
	//Вы накладываете на врага эввект
	//увеличивает урон по врагу на 150% (для всех) и защиту от него на 150%
	//мана 30% откат 3 хода
	public void Challenge()
	{

	}

	//Смена ауры
	//вы меняете ауру
	//мана 20%
	public void ChangeAura(string aura)
	{
		_currentAura = aura;
	}

	//Божественное усиление ауры
	//усиление ауры на 1 ход
	//мана 30% откат 2
	public void StrengtheningAura()
	{

	}


	//ПАСИВКИ (9) 
	//
	//всего можно взять 6 пасивок
	//
	//2 уровень - 1 пассивка
	//5 уровень - 1 пассивки
	//8 уровень - 2 пассивки
	//9 уровень - 2 пассивки
	//
	//на 10 уровне даются все оставшиися пасивки, но в бой можно взять 6 пасивок

	//Уссилиные доспехи
	//ваша защита увеличинна на 20%
	public void IncreaseArmor()
	{

	}

	//Уссилиный урон
	//ваша урон увеличинна на 20%
	public void IncreaseDamage()
	{

	}

	//Уссилиное лечение
	//ваше лечение увеличинна на 20%
	public void IncreaseHeal()
	{

	}

	//Божественная cкорость
	//Ваша инициатива увеличина на 10
	public void DivinieSpeedd()
	{

	}

	//Магическая броня
	//В начале боя на 4 хода ваша защита увеличина на 100%
	public void MagicArmor()
	{

	}

	//Божественное покровительство
	//если ваше хп упадет ниже 25% активируеться божественый щит на 1 ход
	//откат 5 ходов
	public void DivineProtection()
	{

	}

	//Святые удары
	//Ваша каждая атака востанавливает 10% маны
	public void HolyHits()
	{

	}

	//Бестрашный
	//Вас нельзя испугать
	public void Fearless()
	{

	}

	//Сила света
	//Если враг повержен, вы усиляете свои атаки на 150% на 2 хода
	public void LightsPower()
	{

	}

	//Ауры (даються автоматически)
	//после взятие скила на Ауры, даеться все ауры

	//Аура являеться уникальной механикой для паладина,
	//у других персонажей будут свои уникальные механики

	//Аура защиты
	//защита союзников увеличина на 120%
	public void AuraDefence()
	{

	}
	//Аура урона
	//урон союзников увеличин на 120%
	public void AuraAttack()
	{

	}
	//Аура лечения
	//каждый ход аура востонавливает 5% хп всем союзникам
	public void AuraHeal()
	{

	}
	//Аура скорости
	//каждый ход аура добовляет 5 инициативы всем союзникам
	public void AuraSpeed()
	{

	}
}
