using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState gameState;

    public enum GameState
    {
        StartUp,
        ChoseNextChunk,
        StartNewWave,
        EndOfWave,
        GameOver,
        Victory
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
                ChunkManager.Instance.HideAllEmptyChunks();
                break;
            case GameState.ChoseNextChunk:
                InputManager.Instance.EnablePlayerInput();
                ChunkManager.Instance.ShowAllEmptyChunks();
                break;
            case GameState.StartNewWave:
                WaveManager.Instance.SpawnNewWave();
                ChunkManager.Instance.HideAllEmptyChunks();
                UIManager.Instance.UpdateWaveText();
                break;
            case GameState.EndOfWave:
                InventoryManager.Instance.currentSelectedCard = null;

                InventoryManager.Instance.SpawnDrawCards();

                InputManager.Instance.DisablePlayerInput();
                break;
            case GameState.GameOver:
                UIManager.Instance.ActivateGameOverUI();
                Time.timeScale = 0f;
                break;
            case GameState.Victory:
                UIManager.Instance.ActivateVictoryUI();
                Time.timeScale = 0f;
                break;
            default:
                Debug.Log("Gamestate not found");
                break;
        }

        gameState = state;
    }



    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
