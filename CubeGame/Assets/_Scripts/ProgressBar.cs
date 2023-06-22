using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ProgressBar : MonoBehaviour, IProgressable
{
    [SerializeField] private Slider _progressSlider;
    private float _maxX;
    private int CurrentLevelPercent => PlayerPrefs.GetInt($"{LevelsManager.GetCurrentScene()} {Level.Stat.Percent}");
    public bool IsBetterProgress => Mathf.RoundToInt(_progressSlider.value * 100) > CurrentLevelPercent;
    [Inject]
    private void Construct(Finish finish) => _maxX = finish.transform.position.x;
    private void Update() => _progressSlider.value = transform.position.x / _maxX;
    public void TryUpdateLevelPercent()
    {
        if (IsBetterProgress)
        {
            SaveProgress();
        }
    }
    public void SaveProgress()
    {
        PlayerPrefs.SetInt($"{LevelsManager.GetCurrentScene()} {Level.Stat.Percent}", Mathf.RoundToInt(_progressSlider.value * 100));
    }
}
