using Zenject;

public class Star : SpecialModifier
{
    [Inject] private readonly StarCounter _starCounter;
    protected override void Trigger() => _starCounter.AddStar();
}