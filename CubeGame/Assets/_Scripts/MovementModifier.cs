using UnityEngine;
using Zenject;

public abstract class MovementModifier : MonoBehaviour
{
    [Inject] protected readonly Player _player;
    [SerializeField] private AudioSource _audioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trigger();
        _audioSource.Play();
    }
    protected abstract void Trigger();
}
