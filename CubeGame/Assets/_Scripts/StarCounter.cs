using UnityEngine;

public class StarCounter : IProgressable
{
    private int _temporaryStars, _gainedStars;
    private int _thisLevelGainedStars => PlayerPrefs.GetInt($"{LevelsManager.GetCurrentScene()} {Level.Stat.Stars}");
    public int AllStars => _temporaryStars + _gainedStars;
    public bool IsBetterProgress => AllStars > _thisLevelGainedStars;
    public void ResetTempStars() => _temporaryStars = 0;
    public void AddStar() => _temporaryStars++;
    public void CheckpointStars()
    {
        _gainedStars = _temporaryStars;
        ResetTempStars();
    }
    public void TryUpdateLevelStars()
    {
        if (IsBetterProgress)
        {
            SaveProgress();
        }
    }
    public void SaveProgress()
    {
        PlayerPrefs.SetInt("TotalStars", PlayerPrefs.GetInt("TotalStars", 0) + (AllStars - _thisLevelGainedStars));
        PlayerPrefs.SetInt($"{LevelsManager.GetCurrentScene()} {Level.Stat.Stars}", AllStars);
    }
}