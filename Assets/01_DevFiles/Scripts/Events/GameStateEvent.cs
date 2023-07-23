using System;

public class GameStateEvent
{
    public static event Action<GameState> OnChangeGameState;
    public static void Fire_OnChangeGameState(GameState gameState) { OnChangeGameState?.Invoke(gameState); }

    public static event Action OnBeginGame;
    public static void Fire_OnBeginGame() { OnBeginGame?.Invoke(); }

    public static event Action OnPlayGame;
    public static void Fire_OnPlayGame() { OnPlayGame?.Invoke(); }

    public static event Action OnWinGame;
    public static void Fire_OnWinGame() { OnWinGame?.Invoke(); }

    public static event Action OnLoseGame;
    public static void Fire_OnLoseGamee() { OnLoseGame?.Invoke(); }


}
