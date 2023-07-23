using UnityEngine;
using System.Collections;

public class GameManager : MonoSingleton<GameManager>
{
    public GameState gameState;

    protected override void Awake()
    {
        base.Awake();
        GameStateEvent.OnChangeGameState += OnChangeGameState;
    }

    private void Start()
    {
        OnChangeGameState(GameState.Begin);
    }

    void OnChangeGameState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.Begin:
                GameStateEvent.Fire_OnBeginGame();
                break;
            case GameState.Play:
                GameStateEvent.Fire_OnPlayGame();
                break;

            case GameState.Win:
                GameStateEvent.Fire_OnWinGame();
                break;

            case GameState.Lose:
                GameStateEvent.Fire_OnLoseGamee();
                break;

            default:
                break;
        }
    }



    private void OnDisable()
    {
        GameStateEvent.OnChangeGameState -= OnChangeGameState;
    }
}


public enum GameState
{
    Begin,
    Play,
    Win,
    Lose
}

