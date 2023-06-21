using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public static Vector2 CheckpointPosition { get; private set; }
    private void Awake()
    {
        CheckpointPosition = new(0, 0.5f);
        _spriteRenderer.color = Color.red;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckpointPosition = collision.transform.position;
        _spriteRenderer.color = Color.green;
    }
}
