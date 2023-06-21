
public class Jumpy : MovementModifier
{
    protected override void Trigger() => _player.Jumpied();
}
