using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttemptCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textAttempts;
    public static int CurrentAttempts { get; private set; }
    private void Awake()
    {
        IncrementAttempts();
    }
    private void OnEnable()
    {
        Player.Respawned += IncrementAttempts;
    }
    private void OnDisable()
    {
        Player.Respawned -= IncrementAttempts;
    }
    private void IncrementAttempts()
    {
        _textAttempts.text = $"Attempt {++CurrentAttempts}";
        transform.position = Checkpoint.CheckpointPosition + new Vector2(3, 0);
    }
}
