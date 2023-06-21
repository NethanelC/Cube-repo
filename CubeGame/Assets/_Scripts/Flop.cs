public class Flop : SpecialModifier
{
    protected override void Trigger() => _player.Flopped();
}
