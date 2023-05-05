using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelsManager : MonoBehaviour
{
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(GetCurrentScene());
    }
    public void LoadAScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public static string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }
}
