using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private Levels _levels;
    public static int MaximumStars { get; private set; }
    public void ReloadScene() => SceneManager.LoadScene(GetCurrentScene());
    public void LoadLevel(int level, Level.Difficulty difficulty)
    {
        MaximumStars = _levels.GetLevel(level - 1).MaximumStars(difficulty);
        SceneManager.LoadScene($"{level} {difficulty}");
    }
    public void LoadMenu() => SceneManager.LoadScene("Menu");
    public static string GetCurrentScene() => SceneManager.GetActiveScene().name;
}
