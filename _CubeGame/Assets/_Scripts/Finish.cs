using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Finish : MonoBehaviour
{
    public static Finish Instance { get; private set; }
    public static event Action FinishedGame;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private TextMeshProUGUI _finishTextAttempts, _finishTextStars;
    private void Awake()
    {
        Instance = Instance != null? Instance : this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FinishedGame?.Invoke();
        _finishTextAttempts.text = $"Attempt: {AttemptCounter.CurrentAttempts}";
        _finishTextStars.text = $"{StarCounter.CurrentStars} / {PlayerPrefs.GetInt($"{LevelsManager.GetCurrentScene()} Maximum Stars")}"; 
        if (StarCounter.CurrentStars > PlayerPrefs.GetInt(LevelsManager.GetCurrentScene(), 0))
        {
            PlayerPrefs.SetInt("TotalStars", PlayerPrefs.GetInt("TotalStars", 0) + (StarCounter.CurrentStars - PlayerPrefs.GetInt(LevelsManager.GetCurrentScene(), 0)));
            PlayerPrefs.SetInt(LevelsManager.GetCurrentScene(), StarCounter.CurrentStars);
        }
        _finishPanel.SetActive(true);
    }
}

