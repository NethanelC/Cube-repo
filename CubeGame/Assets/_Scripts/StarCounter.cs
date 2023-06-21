using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCounter
{
    public StarCounter()
    {
        Player.Death += ResetTempStars;
    }
    public int ThisLevelStars => PlayerPrefs.GetInt($"{LevelsManager.GetCurrentScene()} {Level.Stat.Stars}");
    private int _temporaryStars, _gainedStars;
    public int AllStars => _temporaryStars + _gainedStars;
    public void ResetTempStars() => _temporaryStars = 0;
    public void AddStar() => _temporaryStars++;
    public void TryUpdateLevelStars()
    {
        if (AllStars > ThisLevelStars)
        {
            PlayerPrefs.SetInt("TotalStars", PlayerPrefs.GetInt("TotalStars", 0) + (AllStars - ThisLevelStars));
            PlayerPrefs.SetInt($"{LevelsManager.GetCurrentScene()} {Level.Stat.Stars}", AllStars);
        }
    }
}