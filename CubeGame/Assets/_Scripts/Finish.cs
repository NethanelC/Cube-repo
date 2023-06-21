using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class Finish : MonoBehaviour
{
    [Inject] private readonly StarCounter _starCounter;
    [Inject] private readonly AttemptCounter _attemptCounter;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private TextMeshProUGUI _finishTextAttempts, _finishTextStars;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetInt($"{LevelsManager.GetCurrentScene()}{Level.Stat.Percent}", 100);
        _finishTextAttempts.text = $"Attempt: {_attemptCounter.CurrentAttempts}";
        _finishTextStars.text = $"{_starCounter.AllStars} / {_starCounter.ThisLevelStars}";
        _starCounter.TryUpdateLevelStars();
        _finishPanel.SetActive(true);
    }
}

