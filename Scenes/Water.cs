using Godot;
using System;

public partial class Water : StaticBody3D
{
    private Label3D _label;
    private Reactor _reactor;
    private int _ghostsReceived;
    private float _coolCounter;
    public int GhostsReceived
    {
        get => _ghostsReceived;
        set
        {
            _ghostsReceived = value;
            UpdateText();
        }
    }
    [Export] public int GhostsRequired { get; set; } = 3;
    [Export] public float SecondsToCool { get; set; } = 0;
    public int Remaining => Math.Max(GhostsRequired - GhostsReceived, 0);

    public override void _Ready()
    {
        _label = GetNode<Label3D>("./label");
        _reactor = GetNode<Reactor>("../../reactor");
        var area3D = GetNode<Area3D>("./Area3D");
        area3D.BodyEntered += OnBodyEntered;
        GhostsReceived = 0;
    }

	private void OnBodyEntered(Node3D body)
    {
        if (body is Enemy enemy)
        {
            AbsorbGhost(enemy);
        }
    }

    private void AbsorbGhost(Enemy enemy)
    {
        _coolCounter = 0;
        GhostsReceived++;
        enemy.Explode();
        _reactor.CheckVictoryCondition();
    }

    private void UpdateText()
    {
        _label.Text = $"{Remaining}";
    }

    public override void _Process(double delta)
    {
        if (SecondsToCool > 0
            && GhostsReceived > 0)
        {
            _coolCounter += (float)delta;
            if (_coolCounter > SecondsToCool)
            {
                _coolCounter -= SecondsToCool;
                GhostsReceived--;
            }
        }
    }
}
