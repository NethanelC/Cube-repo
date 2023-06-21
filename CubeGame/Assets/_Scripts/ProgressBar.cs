using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider _progressSlider;
    private Transform _playerTransform;
    [Inject]
    private void Construct(Finish finish, Player player)
    {
        _playerTransform = player.transform;
        _maxX = finish.transform.position.x;
    }
    private float _maxX;
    private void OnEnable() => Player.Death += TryUpdateLevelPercent;
    private void OnDisable() => Player.Death -= TryUpdateLevelPercent;
    private void Update() => _progressSlider.value = _playerTransform.position.x / _maxX;
    private void TryUpdateLevelPercent() => PlayerPrefs.SetInt($"{LevelsManager.GetCurrentScene()}{Level.Stat.Percent}", Mathf.RoundToInt(_progressSlider.value * 100));
}
