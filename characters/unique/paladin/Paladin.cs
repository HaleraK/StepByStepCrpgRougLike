using Godot;
using StepByStepCrpgRougLike.characters;
using System;
using static Character;
using static Control;

public partial class Paladin : Area2D
{
	private string _currentAura;
	private Character _character;
	private AbilityStats _abilityStats;


    public override void _Ready()
	{
        _character = GetParent<Character>();
	}

	public override void _Process(double delta)
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
	public AbilityStats NormalAttack()
	{
        AbilityStats stats = new("Atk","Mob");
		stats.TargetCount = 1;

		/*
		int m = 1;
		for (int i = 0; i < 6; i++)
		{
            stats.AddTarget(i + 1 , m);
			if (m % 2 == 1)
			{
				m++;
			}
        }
		*/

        float dammage = 0;
		dammage += _character.Atk;

		stats.Damage = dammage;

		return stats;
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
