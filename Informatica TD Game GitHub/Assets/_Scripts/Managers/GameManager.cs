using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        CreateMap,
        StartNewWave,
        EndOfWave
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SwitchGameState(GameState.CreateMap);
    }

    public void SwitchGameState(GameState state)
    {
        switch (state)
        {
            case GameState.CreateMap:
                ChunckManager.Instance.TempPathSpawner();
                break;
            case GameState.StartNewWave:
                TempEnemySpawner.Instance.SpawnEnemy();
                break;
            case GameState.EndOfWave:
                CardDrawManager.Instance.SpawnCards();
                break;
            default:
                Debug.Log("Gamestate not found");
                break;
        }
    }
}
