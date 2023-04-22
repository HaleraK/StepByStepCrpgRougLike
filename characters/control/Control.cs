using Godot;
using System;

public partial class Control : Node
{
	public string WhoseTurn { set; get; }
    public string TypeTurn { set; get; } //character mob non
    private string[] _posibleTargets;

    public float Damage { set; get; }

    public bool Pause = false;

    private readonly static string[] POSIBLE_TYPES
        = { "NormalAtk", "Ability", "Ability2", "Ability3", "Ability4" };

    private Characters[] _allChars;

    public class Characters
    {
        public string Character { get; set; }
        public float Init { get; set; }
        public string Type { get; set; }
        public bool IsTurn { get; set; }

        public Characters(string characters, float init, string type)
        {
            Character = characters;
            Init = init;
            Type = type;
        }

        public Characters(string characters)
        {
            Character = characters;
        }
    }
	
	public override void _Ready()
	{
		GetCharactersList();
	}

    public override void _Process(double delta)
    {
        if (!Pause) { 
            GetCharactersInit();
            SetTouglePause();
            SetTurn();

            if (TypeTurn == "Character")
            {
                LinkButton();
            }
        }   
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
            node = GetNode("../ConteinerOfChars/" + child.Name);
            _allChars[i] = new Characters((string)child.Name);
            _allChars[i].Type = (string)node.Get("Type");
            i++;
        }
	}

    public void SetInit()
    {
        var node = GetNode("../ConteinerOfChars/" + WhoseTurn);
        node.Set("InitiativeCurrent", 0);
        node.Set("PauseForAction", false);
    }

    public void GetCharactersInit() 
    {
        foreach (var child in _allChars)
        {
            var node = GetNode("../ConteinerOfChars/" + child.Character);
            child.Init = (float)node.Get("InitiativeCurrent");
            
        }

    }

	public void SetTouglePause()
	{
        foreach (var child in _allChars)
        {
            if (child.Init >= 100)
            {
                Pause = true;
                
                break;
            }
            else if (child.Init < 100)
            {
                Pause = false;
            }
        }

        foreach (var child in _allChars)
        {
            var node = GetNode("../ConteinerOfChars/" + child.Character);
            node.Set("PauseForAction", Pause);
        }
        
    }

    public void PauseTougle()
    {

        foreach (var child in _allChars)
        {
            var node = GetNode("../ConteinerOfChars/" + child.Character);
            node.Set("PauseForAction", Pause);
        }

    }

    public void SetTurn()
    {
        foreach (var child in _allChars)
        {
            if (child.Init >= 100)
            {
                WhoseTurn = child.Character;
                TypeTurn = child.Type;
                break;
            }
        }
    }

    public void LinkButton()
    {
        var node = GetNode("../ContainerOfActionButtons/NormalAtk");
        node.Set("WhoseTurn", WhoseTurn);
        node.Set("PauseForAction", Pause);
        /*
        foreach (var type in POSIBLE_TYPES)
        {
            var node = GetNode("../ContainerOfActionButtons/" + type);
            node.Set("_whoseTurn", _whoseTurn);
            node.Set("_pauseForAction", _pause);
        }
        */
    }

    public void EndTurn()
    {
        SetInit();
        Pause = false;
        PauseTougle();
        Damage = 0;
        WhoseTurn = "";
        Array.Resize(ref _posibleTargets, 0);
      
    }

}
