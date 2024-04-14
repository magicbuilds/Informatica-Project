using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> popUps;
    private int popUpIndex = -1;
    private bool completed = false;

    private float waitTime = 1.3f;
    private float timeWaited;

    [SerializeField] private Camera cam;
    public float valueToCheck;

    [SerializeField] private GameObject deck;
    [SerializeField] private GameObject cardDrawUI;

    private void Start()
    {
        GoToNextPopUp();
        deck.SetActive(false);
    }

    private void Update()
    {
        if (completed)
        {
            if (timeWaited >= waitTime)
            {
                GoToNextPopUp();
                timeWaited = 0;
            }
            else
            {
                timeWaited += Time.deltaTime;
                return;
            }
        }

        if (popUpIndex == 0)
        {
            if ((Vector2)cam.transform.position != Vector2.zero)
            {
                completed = true;
                valueToCheck = cam.orthographicSize;
            }
        }
        if (popUpIndex == 1)
        {
            if (cam.orthographicSize != valueToCheck)
            {
                completed = true;
            }
        }
        if (popUpIndex == 2)
        {
            deck.SetActive(true);
            if (InventoryManager.Instance.currentSelectedCard != null)
            {
                completed = true;
            }
        }
        if (popUpIndex == 3)
        {
            if (UpgradeManager.Instance.placedTowers.Count > 0)
            {
                completed = true;
            }
        }
        if (popUpIndex == 4)
        {
            if (WaveManager.Instance.waveIndex > 0)
            {
                completed = true;
            }
        }
        if (popUpIndex == 5)
        {
            if (WaveManager.Instance.enemiesLeftInThisWave == 0)
            {
                GoToNextPopUp();
            }
            if (WaveManager.Instance.waveIndex > 1)
            {
                GoToNextPopUp();
            }
        }
        if (popUpIndex == 6)
        {
            cardDrawUI.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
                completed = true;
            }
        }
        if (popUpIndex == 7)
        {
            cardDrawUI.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                completed = true;
            }
        }
        if (popUpIndex == 8)
        {
            if (Input.GetMouseButtonDown(0))
            {
                completed = true;
            }
        }
        if (popUpIndex == 9)
        {
            if (WaveManager.Instance.waveIndex > 3)
            {
                if (WaveManager.Instance.enemiesLeftInThisWave == 0)
                {
                    completed = true;
                }
            }
        }
        if (popUpIndex == 10)
        {
            if (WaveManager.Instance.waveIndex > 4)
            {
                completed = true;
            }
        }

    }

    private void GoToNextPopUp()
    {
        popUpIndex++;

        completed = false;

        foreach (GameObject popUp in popUps)
        {
            popUp.SetActive(false);
        }
        popUps[popUpIndex].SetActive(true);
    }
}
