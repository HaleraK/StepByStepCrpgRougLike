using Godot;
using StepByStepCrpgRougLike.characters;
using System;
using System.Linq;
using static Ability;
using static Character;
using static Control;

public partial class Paladin : Area2D
{
	private string _currentAura;
	private Character _character;
	private Ability _ability;
    private Ability[] _abilitiesClass;
    private string[] chosenAbilities = { "TripleAtk", "ManaAtk", "HealAlly", "SelfHeal" };


    public override void _Ready()
	{
        _character = GetParent<Character>();
		CreateAbilitiesClass();

    }

	public override void _Process(double delta)
	{
	}

    public void CreateAbilitiesClass()
	{
		Array.Resize(ref _abilitiesClass, 5);

		_abilitiesClass[0] = new Ability("NormalAttack");
        _abilitiesClass[1] = new Ability("TripleAtk");
        _abilitiesClass[2] = new Ability("ManaAtk");
        _abilitiesClass[3] = new Ability("HealAlly");
        _abilitiesClass[4] = new Ability("SelfHeal");
    }

    public Ability CallAbilityByName(string name)
    {
        return (Ability)this.Call(name);
    }

    public Ability GetByNameAbility(string nameAbility)
	{
        return _abilitiesClass.Where(a => a.NameAbility == nameAbility).FirstOrDefault();
    }

	public void DecrementCoolDowns()
	{
		foreach (var ability in _abilitiesClass)
		{
			if (ability.CoolDown > 0) 
			{ 
				ability.CoolDown--;
			} else
			{
				ability.OnCoolDown = false;
            }
		}
	}

	public bool IsOnCoolDown(Ability ability)
	{
        if (ability.CoolDown > 0 && ability.OnCoolDown)
        {
            return true;
        }
		return false;
    }

    public void TougleVisible()
	{
		Visible = !Visible;
	}

    public Ability Ability(int n)
    {
        return CallAbilityByName(chosenAbilities[n]);

    }

    //АКТИВИ (1 + 13)
    //обычная атака всегда доступна
    //Всего доступно 12 абилок
    //За каждый уровень можно 1 абилку
    //На 10 уровне даються все оставшиися абилки
    //сняражать в бой можно 4 абилки не считая обычной атаки

    //Удар света
    //обычная атака 100% атаки
    public Ability NormalAttack()
	{
		Ability stats = GetByNameAbility("NormalAttack");

        stats.Selected = true;
        stats.Type = "Atk";
        stats.TargetType = "Mob";
		stats.SetPriorityOfTargets("Front");
        stats.CharSource = _character;

        stats.TargetCount = 1;
		stats.CountActions = 1;
        stats.CoolDown = 0;

        float dammage = 0;
		dammage += _character.Atk;

		stats.Damage = dammage;

		return stats;
	}

	//Тройное правосудие
	//3 атаки со 80% силы
	//откат 3 хода
	public Ability TripleAtk()
	{
		Ability stats = GetByNameAbility("TripleAtk");

        if (IsOnCoolDown(GetByNameAbility("TripleAtk")))
		{
			return stats;
		}

        GetByNameAbility("TripleAtk").CoolDown = 3;
        stats.Selected = true;
        stats.Type = "Atk";
        stats.TargetType = "Mob";
        stats.CharSource = _character;
        stats.SetPriorityOfTargets("Front");

        stats.TargetCount = 1;
        stats.CountActions = 3;
		stats.CoolDown = 3;

        float dammage = 0;
        dammage += _character.Atk;
        dammage *= (float)0.8;

        stats.Damage = dammage;

        return stats;
    }

	//Божественное вмешательство
	//Луч света который бьет любую цель 150% силы
	//мана 30%
	public Ability ManaAtk()
	{
		Ability stats = GetByNameAbility("ManaAtk");
        stats.ManaCost = 40;

		if (_character.Mana < stats.ManaCost)
		{
            return stats;
        }

        stats.Selected = true;
        stats.Type = "Atk";
        stats.TargetType = "Mob";
        stats.CharSource = _character;
        stats.SetPriorityOfTargets("Any");

        stats.TargetCount = 1;
        stats.CountActions = 1;
        stats.CoolDown = 0;

        float dammage = 0;
        dammage += _character.Atk;
        dammage *= (float)0.8;

        stats.Damage = dammage;

        return stats;
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
	public Ability HealAlly()
	{
        Ability stats = GetByNameAbility("HealAlly");
        stats.Selected = true;
        stats.Type = "Heal";
        stats.TargetType = "Character";
        stats.CharSource = _character;
        stats.SetPriorityOfTargets("Any");

        stats.TargetCount = 1;
        stats.CountActions = 1;
        stats.CoolDown = 0;
        stats.ManaCost = 60;

        float heal = 0;
        heal += _character.Atk;
        heal *= (float)2;

        stats.Heal = heal;

        return stats;
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
	public Ability SelfHeal()
	{
		Ability stats = GetByNameAbility("SelfHeal");
        stats.Selected = true;
        stats.Type = "Heal";
        stats.TargetType = "Self";
        stats.CharSource = _character;

        stats.TargetCount = 1;
        stats.CountActions = 1;
        stats.CoolDown = 0;
        stats.ManaCost = 40;

        float heal = 0;
        heal += _character.Atk;
        heal *= (float)2;

        stats.Heal = heal;

        return stats;
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
