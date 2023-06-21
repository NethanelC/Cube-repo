using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttemptCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textAttempts;
    public int CurrentAttempts { get; private set; }
    private void Awake() => IncrementAttempts();
    private void OnEnable() => Player.Death += IncrementAttempts;
    private void OnDisable() => Player.Death -= IncrementAttempts;
    private void IncrementAttempts()
    {
        CurrentAttempts++;
        _textAttempts.text = $"Attempt {CurrentAttempts}";
        transform.position = Checkpoint.CheckpointPosition + new Vector2(3, 0);
    }
}
