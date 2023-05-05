using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCounter : MonoBehaviour
{
    private int _tempStars;
    public static int CurrentStars { get; private set; }
    private void OnEnable()
    {
        Player.Respawned += ResetTempStars;
        Star.CollectStar += AddStar;
        Checkpoint.ReachedCheckPoint += SetCurrentStars;
        Finish.FinishedGame += SetLevelStars;
    }
    private void OnDisable()
    {
        Player.Respawned -= ResetTempStars;
        Star.CollectStar -= AddStar;
        Checkpoint.ReachedCheckPoint -= SetCurrentStars;
        Finish.FinishedGame -= SetLevelStars;
    }
    private void SetCurrentStars()
    {
        CurrentStars += _tempStars;
        ResetTempStars();
    }
    private void ResetTempStars()
    {
        _tempStars = 0;
    }
    private void AddStar()
    {
        _tempStars++;
    }
    private void SetLevelStars()
    {
        SetCurrentStars();
        if (PlayerPrefs.GetInt($"{LevelsManager.GetCurrentScene()} Current Stars", 0) < CurrentStars)
        {
            PlayerPrefs.SetInt($"{LevelsManager.GetCurrentScene()} Current Stars", CurrentStars);
        }
    }
}