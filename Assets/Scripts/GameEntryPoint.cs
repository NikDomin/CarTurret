using UnityEngine;

public class GameEntryPoint
{
    private static GameEntryPoint instance;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutoStartGame()
    {
        instance = new GameEntryPoint();
        instance.RunGame();
    }

    private GameEntryPoint()
    {
        
    }

    private void RunGame()
    {
        
    }
}

