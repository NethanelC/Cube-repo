using UnityEngine;

public class StarCounter
{
    private int _temporaryStars, _gainedStars;
    private int _thisLevelGainedStars => PlayerPrefs.GetInt($"{LevelsManager.GetCurrentScene()} {Level.Stat.Stars}");
    public int AllStars => _temporaryStars + _gainedStars;
    public void ResetTempStars() => _temporaryStars = 0;
    public void AddStar() => _temporaryStars++;
    public void CheckpointStars()
    {
        _gainedStars = _temporaryStars;
        ResetTempStars();
    }
    public void TryUpdateLevelStars()
    {
        if (AllStars > _thisLevelGainedStars)
        {
            PlayerPrefs.SetInt("TotalStars", PlayerPrefs.GetInt("TotalStars", 0) + (AllStars - _thisLevelGainedStars));
            PlayerPrefs.SetInt($"{LevelsManager.GetCurrentScene()} {Level.Stat.Stars}", AllStars);
        }
    }
}