using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public static Vector2 CheckpointPosition { get; private set; }
    public static event Action ReachedCheckPoint;
    private void Awake()
    {
        _spriteRenderer.color = Color.red;
        CheckpointPosition = new(0, 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReachedCheckPoint?.Invoke();
        CheckpointPosition = collision.transform.position;
        _spriteRenderer.color = Color.green;
    }
}
