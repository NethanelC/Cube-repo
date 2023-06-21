
public class Speedy : MovementModifier
{
    protected override void Trigger() => _player.Speedied();
}
