using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private Finish _finish;
    [SerializeField] private AttemptCounter _attemptCounter;
    public override void InstallBindings()
    {
        PlayerInputs inputs = new();
        inputs.Enable();
        Container.BindInstance(inputs);
        Container.BindInstance(new StarCounter());
        Container.BindInstance(_player);
        Container.BindInstance(_finish);
        Container.BindInstance(_attemptCounter);
    }
}