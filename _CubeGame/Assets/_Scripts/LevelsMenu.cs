using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelsMenu : MonoBehaviour
{
    [SerializeField] private Levels _levels;
    [SerializeField] private LevelButton _levelButton;
    [SerializeField] private RectTransform _levelsTransform;
    [SerializeField] private TextMeshProUGUI _selectedLevel, _percentNormal, _percentHard, _starsNormal, _starsHard;
    [SerializeField] private Slider _sliderNormal, _sliderHard; 
    [SerializeField] private Button _buttonPrev, _buttonNext;
    private int _currentLevel = 1;
    private void Awake()
    {  
        for (int i = 0; i < _levels._levelImage.Count; ++i)
        {
            Instantiate(_levelButton, _levelsTransform).Init(i + 1, _levels._levelImage[i]);
        }
        SetSpecificLevelDetails();
        _buttonNext.onClick.AddListener(() => 
        {
            _currentLevel = (_currentLevel++ % _levels._levelImage.Count) + 1;
            SetSpecificLevelDetails();
        });
        _buttonPrev.onClick.AddListener(() =>
        {
            _currentLevel = (_currentLevel-- % _levels._levelImage.Count) + 1;
            SetSpecificLevelDetails();
        });
    }
    private void SetSpecificLevelDetails()
    {
        _levelsTransform.DOAnchorPosX((_currentLevel - 1) * -1600, 0.5f).SetEase(Ease.Linear).OnComplete(() => 
        {
            _selectedLevel.text = $"Level {_currentLevel}";
            _percentNormal.text = $"{_sliderNormal.value = PlayerPrefs.GetInt($"{_currentLevel} N Percent")} %";
            _percentHard.text = $"{_sliderHard.value = PlayerPrefs.GetInt($"{_currentLevel} H Percent")} %";
            _starsNormal.text = $"{PlayerPrefs.GetInt($"{_currentLevel} N Current Stars")} / {PlayerPrefs.GetInt($"{_currentLevel} N Maximum Stars")}";
            _starsHard.text = $"{PlayerPrefs.GetInt($"{_currentLevel} H Current Stars")} / {PlayerPrefs.GetInt($"{_currentLevel} H Maximum Stars")}";
        });
    }
}
