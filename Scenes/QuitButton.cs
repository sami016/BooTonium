using Godot;
using System;

public partial class QuitButton : Button
{
    public override void _Ready()
    {
        Pressed += OnPressed;
    }

    private void OnPressed()
    {
        GetTree().Quit();
    }
}
