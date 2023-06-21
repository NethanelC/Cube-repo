using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelsMenu : MonoBehaviour
{
    [SerializeField] private Levels _levels;
    [SerializeField] private LevelButton _levelButtonPrefab;
    [SerializeField] private RectTransform _levelsTransform;
    [SerializeField] private TextMeshProUGUI _selectedLevelText, _percentNormal, _percentHard, _starsNormal, _starsHard;
    [SerializeField] private Slider _sliderNormal, _sliderHard; 
    [SerializeField] private Button _buttonPrev, _buttonNext;
    private Level _selectedLevel => _levels.GetLevel(_currentLevel - 1);
    private int _currentLevel = 1;
    private void Awake()
    {
        Time.timeScale = 1;
        for (int i = 0; i < _levels.LevelsAmount; ++i)
        {
            Instantiate(_levelButtonPrefab, _levelsTransform).Init(i, _levels.GetLevel(i));
        }
        UpdateSelectedLevelStats();
        _buttonNext.onClick.AddListener(() => 
        {
            _currentLevel = _currentLevel++ % _levels.LevelsAmount + 1;
            UpdateSelectedLevelStats();
        });
        _buttonPrev.onClick.AddListener(() =>
        {
            _currentLevel = _currentLevel-- % _levels.LevelsAmount + 1;
            UpdateSelectedLevelStats();
        });
    }
    private void UpdateSelectedLevelStats()
    {
        _levelsTransform.DOAnchorPosX((_currentLevel - 1) * -1600, 0.5f).SetEase(Ease.Linear).OnComplete(() => 
        {
            _selectedLevelText.text = $"Level {_currentLevel}";
            _sliderNormal.value = _levels.GetStatsOfLevel(_currentLevel, Level.Difficulty.Normal, Level.Stat.Percent);
            _percentNormal.text = $"{_sliderNormal.value} %";
            _sliderHard.value = _levels.GetStatsOfLevel(_currentLevel, Level.Difficulty.Hard, Level.Stat.Percent);
            _percentHard.text = $"{_sliderHard.value} %";
            _starsNormal.text = $"{_levels.GetStatsOfLevel(_currentLevel, Level.Difficulty.Normal, Level.Stat.Stars)} / {_selectedLevel.MaximumStars(Level.Difficulty.Normal)}";
            _starsHard.text = $"{_levels.GetStatsOfLevel(_currentLevel, Level.Difficulty.Hard, Level.Stat.Stars)} / {_selectedLevel.MaximumStars(Level.Difficulty.Hard)}";
        }); 
    }
}
