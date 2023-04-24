using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class PosibleCharClasses : Node
{
	public string NameClass { get; set; }
	private Paladin _paladin;
	private Slime _slime;
    private PosibleCharClasses _self;

    public PosibleCharClasses(string nameClass)
	{
        NameClass = nameClass;
        LinkCharClass(NameClass);
        _self = this;
    }

    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void LinkCharClass(string nameClass) {
        switch (nameClass)
        {
            case "Paladin":
                _paladin = GetNode<Paladin>(nameClass);
                break;
            case "Slime":
                _slime = GetNode<Slime>(nameClass);
                break;
        }
    }

    public void GetCharClass()
    {
        _self.Call("Get" + NameClass + "Node");
    }

    private Paladin GetPaladinNode()
    {
        return _paladin;
    }
    private Slime GetSlimeNode()
    {
        return _slime;
    }
}
