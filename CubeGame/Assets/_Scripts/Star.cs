using Zenject;
public class Star : BaseTrigger
{
    [Inject] private StarCounter _starCounter;
    protected override void Trigger() => _starCounter.AddStar();
}