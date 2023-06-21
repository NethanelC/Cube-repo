public class Flip : SpecialModifier
{
    protected override void Trigger() => _player.Flipped();
}
