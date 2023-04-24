using Godot;
using System;

public partial class SkillList : Node
{
    class Skill
    {
        public string Name { get; set; }
        public bool Learned { get; set; } = false;
        public string Requiered { get; set; }
        public string Description { get; set; }

        public Skill(string name, bool learned, string requiered)
        {
            Name = name;
            Learned = learned;
            Requiered = requiered;
        }
        public Skill(string name, bool learned)
        {
            Name = name;
            Learned = learned;
            Requiered = "";
        }
    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
