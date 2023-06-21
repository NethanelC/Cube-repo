using UnityEngine;
using Zenject;

public class Checkpoint : MonoBehaviour
{
    [Inject] private readonly StarCounter _starCounter;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public static Vector2 CheckpointPosition { get; private set; }
    private void Awake()
    {
        CheckpointPosition = Vector2.zero;
        _spriteRenderer.color = Color.red;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckpointPosition = collision.transform.position;
        _spriteRenderer.color = Color.green;
        _starCounter.CheckpointStars();
    }
}
