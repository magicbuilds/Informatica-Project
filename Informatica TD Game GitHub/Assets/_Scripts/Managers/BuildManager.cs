using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")] 
    [SerializeField] private GameObject[] buildingPrefabs;

    private int currentSelectedTower = 0;
    private void Awake()
    {
        main = this;
    }

    public GameObject GetSelectedTower()
    {
        return buildingPrefabs[currentSelectedTower];
    }
}
