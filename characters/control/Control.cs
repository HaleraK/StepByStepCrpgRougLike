using Godot;
using System;

public partial class Control : Node
{
	private string _whoseTurn;
	private string _typeTurn; //character mob non
    private string[] _posibleTargets;

    public float Damage { set; get; }

    private bool _pause = false;

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
        if (!_pause) { 
            GetCharactersInit();
            SetTouglePause();
            SetTurn();

            if (_typeTurn == "character")
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
            _allChars[i].Type = (string)node.Get("_type");
            i++;
        }
	}

    public void SetInit()
    {
        var node = GetNode("../ConteinerOfChars/" + _whoseTurn);
        node.Set("_initiativeCurrent", 0);
        node.Set("PauseForAction", false);
    }

    public void GetCharactersInit() 
    {
        foreach (var child in _allChars)
        {
            var node = GetNode("../ConteinerOfChars/" + child.Character);
            child.Init = (float)node.Get("_initiativeCurrent");
            
        }

    }

	public void SetTouglePause()
	{
        foreach (var child in _allChars)
        {
            if (child.Init >= 100)
            {
                _pause = true;
                
                break;
            }
            else if (child.Init < 100)
            {
                _pause = false;
            }
        }

        foreach (var child in _allChars)
        {
            var node = GetNode("../ConteinerOfChars/" + child.Character);
            node.Set("PauseForAction", _pause);
        }
        
    }

    public void Pause()
    {

        foreach (var child in _allChars)
        {
            var node = GetNode("../ConteinerOfChars/" + child.Character);
            node.Set("PauseForAction", _pause);
        }

    }

    public void SetTurn()
    {
        foreach (var child in _allChars)
        {
            if (child.Init >= 100)
            {
                _whoseTurn = child.Character;
                _typeTurn = child.Type;
                break;
            }
        }
    }

    public void LinkButton()
    {
        var node = GetNode("../ContainerOfActionButtons/NormalAtk");
        node.Set("_whoseTurn", _whoseTurn);
        node.Set("_pauseForAction", _pause);
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
        _pause = false;
        Pause();
        Damage = 0;
        _whoseTurn = "";
        Array.Resize(ref _posibleTargets, 0);
      
    }

}
