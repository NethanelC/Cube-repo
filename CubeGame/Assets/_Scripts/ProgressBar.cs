using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider _progressSlider;
    private float _maxX;
    [Inject]
    private void Construct(Finish finish) => _maxX = finish.transform.position.x;
    private void Update() => _progressSlider.value = transform.position.x / _maxX;
    public void TryUpdateLevelPercent() => PlayerPrefs.SetInt($"{LevelsManager.GetCurrentScene()} {Level.Stat.Percent}", Mathf.RoundToInt(_progressSlider.value * 100));
}
