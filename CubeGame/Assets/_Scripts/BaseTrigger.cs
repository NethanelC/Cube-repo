using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public abstract class BaseTrigger : MonoBehaviour
{
    [Inject] protected readonly Player _player;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Collider2D _collider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trigger();
        _collider.enabled = false;
        _audioSource.Play();
        if (_spriteRenderer != null)
        {
            _spriteRenderer.DOFade(0.3f, 0.5f);
        }
    }
    private void OnEnable() => Player.Death += OnPlayerDeath;
    private void OnDisable() => Player.Death -= OnPlayerDeath;
    private void OnPlayerDeath()
    {
        _collider.enabled = true;
        if (_spriteRenderer != null)
        {
            _spriteRenderer.DOFade(1, 0.1f);
        }
    }
    protected abstract void Trigger();
}
