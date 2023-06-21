using UnityEngine;
using Zenject;
using DG.Tweening;

public abstract class SpecialModifier : MonoBehaviour
{
    [Inject] protected readonly Player _player;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trigger();
        _collider.enabled = false;
        _audioSource.Play();
        _spriteRenderer.DOFade(0.3f, 0.5f);
    }
    private void OnEnable() => _player.Respawned += OnPlayerRespawned;
    private void OnDisable() => _player.Respawned -= OnPlayerRespawned;
    private void OnPlayerRespawned()
    {
        _collider.enabled = true;
        _spriteRenderer.DOFade(1, 0.1f);
    }
    protected abstract void Trigger();
}
