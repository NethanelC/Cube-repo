using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelsManager : MonoBehaviour
{
    public void ReloadScene() => SceneManager.LoadScene(GetCurrentScene());
    public void LoadLevel(int level, Level.Difficulty difficulty) => SceneManager.LoadScene($"{level} {difficulty}");
    public void LoadMenu() => SceneManager.LoadScene("Menu");
    public static string GetCurrentScene() => SceneManager.GetActiveScene().name;
}
