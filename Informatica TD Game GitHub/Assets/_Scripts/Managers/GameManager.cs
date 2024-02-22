using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState gameState;

    public enum GameState
    {
        StartUp,
        ChoseNextChunk,
        StartNewWave,
        EndOfWave
    }

    private void Awake()
    {
        Instance = this;
        
    }
    private void Start()
    {
        SwitchGameState(GameState.StartUp);
        SwitchGameState(GameState.ChoseNextChunk);
    }

    public void SwitchGameState(GameState state)
    {
        switch (state)
        {
            case GameState.StartUp:
                ChunkManager.Instance.SpawnFirstChunk();
                break;
            case GameState.ChoseNextChunk:
                InputManager.Instance.EnablePlayerInput();
                InputManager.Instance.DisableUIInput();
                break;
            case GameState.StartNewWave:
                TempEnemySpawner.Instance.SpawnEnemy();
                break;
            case GameState.EndOfWave:
                //CardDrawManager.Instance.SpawnCards();

                //InputManager.Instance.EnableUIInput();
                //InputManager.Instance.DisablePlayerInput();
                break;
            default:
                Debug.Log("Gamestate not found");
                break;
        }

        gameState = state;
    }
}
