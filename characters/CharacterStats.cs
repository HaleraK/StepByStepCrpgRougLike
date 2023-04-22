using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepByStepCrpgRougLike.characters;

internal class CharacterStats
{

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
}
