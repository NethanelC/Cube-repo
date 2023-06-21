public class Flop : BaseTrigger
{
    protected override void Trigger() => _player.Flopped();
}
