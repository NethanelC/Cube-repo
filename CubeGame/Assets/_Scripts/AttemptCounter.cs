using UnityEngine;
using TMPro;
using Zenject;

public class AttemptCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textAttempts;
    public int CurrentAttempts { get; private set; }
    private void Awake() => IncrementAttempts();
    public void IncrementAttempts()
    {
        CurrentAttempts++;
        _textAttempts.text = $"Attempt {CurrentAttempts}";
        transform.position = Checkpoint.CheckpointPosition + new Vector2(-3, 2);
    }
}
