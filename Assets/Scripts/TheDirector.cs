using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class TheDirector : MonoBehaviour
{
    public static TheDirector instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        UpdateGameState(GameState.Player);
    }
    public void Update()
    {

    }



    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Player:
                break;
            case GameState.Wave:
                WaveManager.instance.StartWaves();
                break;
            case GameState.Victory:
                break;
            case GameState.Defeat:
                break;
            case GameState.Pause:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState
    {
        Pause,
        Player,
        Wave,
        Victory,
        Defeat
    }
}