using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState gameState;

    public float waveNum;

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
        //Debug.Log(state.ToString());
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
                WaveManager.Instance.SpawnNewWave();
                break;
            case GameState.EndOfWave:
                InventoryManager.Instance.SpawnDrawCards();
                
                InputManager.Instance.EnableUIInput();
                InputManager.Instance.DisablePlayerInput();

                
                break;
            default:
                Debug.Log("Gamestate not found");
                break;
        }

        gameState = state;
    }
    
}
