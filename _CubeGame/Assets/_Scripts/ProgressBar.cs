using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{  
    [SerializeField] private Slider _slider;
    private Transform _transform;
    private float _xOfFinish;
    //Caching the transform
    private void Awake()
    {
        _transform = transform;     
    }
    private void Start()
    {
        _xOfFinish = Finish.Instance.gameObject.transform.position.x;
    }
    //Subscribing to events
    private void OnEnable()
    {
        Finish.FinishedGame += SetLevelPercent;
        Obstacle.Obstacled += SetLevelPercent;
    }
    //Unsubscribing from events
    private void OnDisable()
    {
        Finish.FinishedGame -= SetLevelPercent;
        Obstacle.Obstacled -= SetLevelPercent;
    }
    private void Update()
    {
        _slider.value = _transform.position.x / _xOfFinish;
    }
    private void SetLevelPercent()
    {
        if (Mathf.RoundToInt(_slider.value * 100) > PlayerPrefs.GetInt($"{LevelsManager.GetCurrentScene()} Percent", 0))
        {
            PlayerPrefs.SetInt($"{LevelsManager.GetCurrentScene()} Percent", Mathf.RoundToInt(_slider.value * 100));
        }
    }
}
