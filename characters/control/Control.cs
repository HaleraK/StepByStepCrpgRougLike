using Godot;
using System;

public partial class Control : Node
{
    private string[] _posibleTargets;
    public bool Pause = false;
    public bool Turn = false;
    public string TypeTurn;

    private AbilityStats _abilityStats;
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

    public void SetInitTo0(Character character)
    {
        character.InitiativeCurrent = 0;
        character.PauseForAction = false;
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

    public void SetAbilityStats(AbilityStats stats) 
    { 
        _abilityStats = stats;
    }

    public AbilityStats GetAbilityStats()
    {
        return _abilityStats;
    }

    public void MobTurn()
    {
        //заглушка
        AbilityStats statsTaken = _whoseTurn.NormalAtk();
        GetNode("../").GetNode<Character>("ConteinerOfChars/Character").TakeDamage((float)statsTaken.Damage);
        EndTurn();
    }

    public void StartTurn(Character character)
    {
        _whoseTurn = character;
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
        Pause = false;
        Turn = false;
        TypeTurn = "";
        SetInitTo0(_whoseTurn);
        _whoseTurn = null;
        _abilityStats = null;
        PauseAll(Pause);

        Array.Resize(ref _posibleTargets, 0);

        foreach (var but in _buttonsAbility)
        {
            but.DelWhoseTurn();
        }
    }

}
