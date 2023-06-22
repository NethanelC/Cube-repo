using UnityEngine;
using TMPro;

public class AttemptCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textAttempts;
    public int CurrentAttempts { get; private set; }
    private Vector2 _offset => new(-3, 2);
    private void Awake() => IncrementAttempts();
    public void IncrementAttempts()
    {
        CurrentAttempts++;
        _textAttempts.text = $"Attempt {CurrentAttempts}";
        transform.position = Checkpoint.CheckpointPosition + _offset;
    }
}
