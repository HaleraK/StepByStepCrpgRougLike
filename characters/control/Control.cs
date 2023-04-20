using Godot;
using System;

public partial class Control : Node
{
	private bool _pauseForAction = false;
	private string[] _characters;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetCaractersList();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CheckInit();
    }

	public void GetCaractersList() 
	{
		var node = GetNode("../ConteinerOfChars");
		var children = node.GetChildren();

		var i = 0;
        Array.Resize(ref _characters, children.Count);
        foreach ( var child in children )
		{
			_characters[i++] = child.Name;
        }

	}

	public void CheckInit()
	{
        foreach (var child in _characters)
        {
			var pause = (bool)GetNode("../ConteinerOfChars/" + child).Get("PauseForAction");
            //GD.Print(child);

            if (pause)
			{
                _pauseForAction = pause;
				break;
            }
        }

		if (_pauseForAction)
        foreach (var child in _characters)
        {
            GetNode("../ConteinerOfChars/" + child).Set("PauseForAction", _pauseForAction);
        }
    }

}
