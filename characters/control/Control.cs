using Godot;
using System;

public partial class Control : Node
{
    private string[] _posibleTargets;
    public bool Pause = false;
    public bool Turn = false;
    public string TypeTurn;

    private Ability _ability;
    private Character _whoseTurn;

    private readonly static string[] POSIBLE_TYPES
        = { "NormalAtk", "Ability1", "Ability2", "Ability3", "Ability4" };

    private Character[] _allChars;
    private ButtonAbility[] _buttonsAbility;

    public override void _Ready()
	{
		GetCharactersList();
        GetButtons();
    }

    public override void _Process(double delta)
    {
           
    }

	public void GetCharactersList() 
	{
        Array.Resize(ref _allChars, 0);
        var node = GetNode("../ConteinerOfChars");
		var children = node.GetChildren();

		var i = 0;
        Array.Resize(ref _allChars, children.Count);
        foreach ( var child in children )
		{
            _allChars[i] = GetNode<Character>("../ConteinerOfChars/" + child.Name);
            _allChars[i].InitChangedTo100 += StartTurn;
            i++;
        }
    }

    public void PauseAll(bool pause)
    {
        for (int i = 0; i< _allChars.Length; i++)
        {
            _allChars[i].PauseForAction = pause;
        }

    }

    public void GetButtons() 
    {
        Array.Resize(ref _buttonsAbility, POSIBLE_TYPES.Length);
        for (var i = 0; i < POSIBLE_TYPES.Length; i++)
        {
            _buttonsAbility[i] = GetNode<ButtonAbility>("../ContainerOfActionButtons/" + POSIBLE_TYPES[i]);
        }
       
    }

    public void SetAbility(Ability stats) 
    { 
        _ability = stats;
    }

    public Ability GetAbility()
    {
        return _ability;
    }

    public void MobTurn()
    {
        //заглушка
        Ability statsTaken = _whoseTurn.NormalAtk();
        GetNode("../").GetNode<Character>("ConteinerOfChars/Character").TakeDamage((float)statsTaken.Damage);
        EndTurn();
    }

    public void StartTurn(Character character)
    {
        _whoseTurn = character;

        SetButtonsSkin(_whoseTurn);

        Pause = true;
        Turn = true;
        TypeTurn = character.Type;
        PauseAll(Pause);

        switch (TypeTurn)
        {
            case "Mob":
                MobTurn();
                break;
            case "Character":
                foreach (var but in _buttonsAbility)
                {
                    but.SetWhoseTurn(_whoseTurn, TypeTurn);
                }
                break;
        }
    }

    public void EndTurn()
    {
        Turn = false;
        _whoseTurn.EndTurn();
        Pause = false;
        TypeTurn = "";
        _whoseTurn = null;
        _ability = null;
        PauseAll(Pause);

        Array.Resize(ref _posibleTargets, 0);

        foreach (var but in _buttonsAbility)
        {
            but.DelWhoseTurn();
        }
    }

    public void SetButtonsSkin(Character character)
    {
        string nameClass = _whoseTurn.NameClass;

        Ability ability = _whoseTurn.NormalAtk() as Ability;
        string abilityName = ability.NameAbility;
        _buttonsAbility[0].SetSkin(nameClass, abilityName);

        ability = _whoseTurn.Ability1() as Ability;
        abilityName = ability.NameAbility;
        _buttonsAbility[1].SetSkin(nameClass, abilityName);

        ability = _whoseTurn.Ability2() as Ability;
        abilityName = ability.NameAbility;
        _buttonsAbility[2].SetSkin(nameClass, abilityName);

        ability = _whoseTurn.Ability3() as Ability;
        abilityName = ability.NameAbility;
        _buttonsAbility[3].SetSkin(nameClass, abilityName);

        ability = _whoseTurn.Ability4() as Ability;
        abilityName = ability.NameAbility;
        _buttonsAbility[4].SetSkin(nameClass, abilityName);
    }

}
