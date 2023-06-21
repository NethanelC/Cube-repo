using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "SO/Levels", order = 1)]
public class Levels : ScriptableObject
{
    [SerializeField] private Level[] _allLevels = new Level[5];
    public int LevelsAmount => _allLevels.Length;
    public Level GetLevel(int level) => _allLevels[level];
    public int GetStatsOfLevel(int level, Level.Difficulty difficulty, Level.Stat stat) => PlayerPrefs.GetInt($"{level} {difficulty} {stat}", 0);
}
[Serializable]
public class Level
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int NormalMaximumStars { get; private set; }
    [field: SerializeField] public int HardMaximumStars { get; private set; }
    public int MaximumStars(Difficulty difficulty) => difficulty switch
    {
        Difficulty.Normal => NormalMaximumStars,
        Difficulty.Hard => HardMaximumStars,
        _ => throw new ArgumentOutOfRangeException(nameof(difficulty), $"Not expected difficulty: {difficulty}"),
    };
    public enum Difficulty
    {
        Normal,
        Hard
    }
    public enum Stat
    {
        Stars,
        Percent
    }
}