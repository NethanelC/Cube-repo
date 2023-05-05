using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Triggers : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Collider2D _collider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Triggered()?.Invoke();
        _collider.enabled = false;
        _audioSource.Play();
        if (_spriteRenderer != null)
        {
            _spriteRenderer?.DOFade(0.3f, 0.5f);
        }
    }
    private void OnEnable()
    {
        Player.Respawned += ToggleColors;
    }
    private void OnDisable()
    {
        Player.Respawned -= ToggleColors;
    }
    private void ToggleColors()
    {
        _collider.enabled = true;
        if (_spriteRenderer != null)
        {
            _spriteRenderer.DOFade(1, 0.1f);
        }
    }
    protected abstract Action Triggered();
}
