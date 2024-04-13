using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCMenu : MonoBehaviour
{
    public static ESCMenu Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ActivateESCMenu()
    {
        Debug.Log("ACtivatedESC");
        Time.timeScale = 0f;

    }

    public void DeactivateESCMenu()
    {
        Debug.Log("deACtivatedESC");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
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
