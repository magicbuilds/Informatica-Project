using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCMenu : MonoBehaviour
{
    public static ESCMenu Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ActivateESCMenu()
    {
        Time.timeScale = 0f;
        UIManager.Instance.SetHoveringState(true);
    }

    public void DeactivateESCMenu()
    {
        Time.timeScale = 1f;
        UIManager.Instance.SetHoveringState(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void ResumeGame()
    {
        UIManager.Instance.DeactivateESCMenu();
    }

    public void ShowStatsScreen()
    {
        UIManager.Instance.ActivateStatsScreen();
    }

    public void ShowOptionsMenu()
    {
        UIManager.Instance.ActivateOptionsMenu();
    }
    public void HideOptionsScreen()
    {
        UIManager.Instance.DeactivateOptionsMenu();
    }
}
