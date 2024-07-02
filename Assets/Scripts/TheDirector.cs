using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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



    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Player:
                break;
            case GameState.Wave:
                break;
            case GameState.Victory:
                break;
            case GameState.Defeat:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState
{
    Player,
    Wave,
    Victory,
    Defeat
}
}