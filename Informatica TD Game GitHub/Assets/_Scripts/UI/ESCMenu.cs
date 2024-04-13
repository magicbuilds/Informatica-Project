using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCMenu : MonoBehaviour
{
    public static ESCMenu Instance;

    [SerializeField] private GameObject escMenu;

    private void Awake()
    {
        Instance = this;
    }

    public void ActivateESCMenu()
    {
        Time.timeScale = 0f;
        escMenu.SetActive(true);

        InputManager.Instance.DisablePlayerInput();
    }

    public void DeactivateESCMenu()
    {
        Time.timeScale = 1f;
        escMenu.SetActive(false);

        InputManager.Instance.EnablePlayerInput();
    }
}
