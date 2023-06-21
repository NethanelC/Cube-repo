public class Flappy : SpecialModifier
{
    protected override void Trigger() => _player.Flappied();
}
