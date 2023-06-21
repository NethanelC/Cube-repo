using UnityEngine;
using TMPro;
using Zenject;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private TextMeshProUGUI _finishTextAttempts, _finishTextStars;
    private StarCounter _starCounter;
    private AttemptCounter _attemptCounter;
    [Inject]
    private void Construct(StarCounter starCounter, AttemptCounter attemptCounter)
    {
        _starCounter = starCounter;
        _attemptCounter = attemptCounter;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerPrefs.SetInt($"{LevelsManager.GetCurrentScene()} {Level.Stat.Percent}", 100);
        _finishTextAttempts.text = $"Attempt: {_attemptCounter.CurrentAttempts}";
        _finishTextStars.text = $"{_starCounter.AllStars} / {LevelsManager.MaximumStars}";
        _starCounter.TryUpdateLevelStars();
        _finishPanel.SetActive(true);
    }
}

