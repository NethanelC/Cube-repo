using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Triggers
{
    public static event Action CollectStar;
    private static int _amountOfStars;
    //Count stars instances
    private void Awake()
    {
        _amountOfStars++;
    }
    //Check if current stars instances are lower than the amount we counted in Awake, Set if yes
    private void Start()
    {
        if (PlayerPrefs.GetInt($"{LevelsManager.GetCurrentScene()} Maximum Stars", 0) < _amountOfStars)
        {
            PlayerPrefs.SetInt($"{LevelsManager.GetCurrentScene()} Maximum Stars", _amountOfStars);
        }
    }
    protected override Action Triggered()
    {
        return CollectStar;
    }
}
