using Godot;
using System;

public partial class LevelProgress : VBoxContainer
{
	private TextureProgressBar _progress;

	[Export] public Reactor Reactor { get; set; }

    public override void _Ready()
	{
		Reactor.CompletePercentageChanged += CompletePercentageChanged;
		_progress = GetNode<TextureProgressBar>("./thermo/progress");
    }

	private void CompletePercentageChanged()
	{
		_progress.Value = Reactor.CompletePercentage;
	}
}
