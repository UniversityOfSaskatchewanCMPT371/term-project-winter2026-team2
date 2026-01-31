using System.Linq;
using UnityEngine;

public enum GameState
{
    Ready,
    Playing,
    Finished,
}

public class WhackAColorData : MonoBehaviour
{
    public Color defaultColor = Color.white;
    protected Color targetColor;
    private Color[] colorPool =
    {
        Color.blue,
        Color.red,
        Color.cyan,
        Color.green,
        Color.yellow,
        Color.magenta,
        Color.black,
        Color.gray,
        Color.white
    };
    
    private GameState gameState = GameState.Ready;
    private int score = 0;
    private int scoreGoal = 10;

    public bool IsFinished()
    {
        return gameState == GameState.Finished;
    }

    public bool IsReady()
    {
        return gameState == GameState.Ready;
    }

    public bool IsPlaying()
    {
        return gameState == GameState.Playing;
    }

    public void SetTargetColor(Color color)
    {
        if (IsPlaying() || IsFinished()) return;
        targetColor = color;
    }

    public void AdjustScore(int amount)
    {
        if (IsPlaying()) return;
        score += amount;

        if (score >= scoreGoal)
        {
            gameState = GameState.Finished;
        }
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
    }

    public void ResetData()
    {
        SetGameState(GameState.Ready);
        SetTargetColor(defaultColor);
        score = 0;
    }

    public Color GetRandomColor()
    {
        return colorPool[Random.Range(0,colorPool.Length - 1)];
    }

    public Color[] GetListOfRandomColors()
    {
        Color[] randomColors = new Color[colorPool.Length];

        for (int i = 0; i < colorPool.Length; i++)
        {
            randomColors.Append<Color>(GetRandomColor());
        }

        return randomColors;
    }
}
