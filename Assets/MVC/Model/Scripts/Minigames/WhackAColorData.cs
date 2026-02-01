using UnityEngine;

public enum GameState
{
    Ready,
    Playing,
    Finished,
}

public class WhackAColorData : MonoBehaviour
{
    /// <summary>
    /// Data Section
    /// </summary>
    private int score = 0;                              // Current score
    private int goal = 1;                               // Minimum score to win

    private float delay = 3f;                           // Delay between color change
    private bool delayDebounce = false;                 // Prevents consecutive color changes
    private float timeout = 6;                       // Amount of time to win the game
    private float timer = 0f;                           // Countdown timer before out of time

    public Color defaultColor = Color.white;            // Default color
    private Color targetColor;                          // Target color to score a point
    private readonly Color[] colorPool =                // Available colors
    {
        new Color(0.8f, 0f, 0f),                        // Red 
        new Color(1f, 0.5f, 0f),                        // Orange 
        new Color(1f, 1f, 0f),                          // Yellow 
        new Color(0f, 0.8f, 0f),                        // Green 
        new Color(0f, 1f, 1f),                          // Cyan 
        new Color(0f, 0.5f, 1f),                        // Sky Blue 
        new Color(0f, 0f, 1f),                          // Blue 
        new Color(0.6f, 0f, 1f),                        // Purple 
        new Color(1f, 0f, 1f)                           // Magenta
    };

    private GameState gameState = GameState.Ready;      // Status of the game

    /// <summary>
    /// Getters/Setters Section
    /// </summary>
    public int Score
    {
        get => score;
        set => score = value;
    }
    public int Goal
    {
        get => goal;
        set => goal = value;
    }
    public float Delay
    {
        get => delay;
        set => delay = value;
    }
    public bool DelayDebounce
    {
        get => delayDebounce;
        set => delayDebounce = value;
    }
    public float Timeout
    {
        get => timeout;
        set => timeout = value;
    }
    public float Timer
    {
        get => timer;
        set => timer = value;
    }
    public Color DefaultColor
    {
        get => defaultColor;
        set => defaultColor = value;
    }
    public Color TargetColor
    {
        get => targetColor;
        set => targetColor = value;
    }
    public GameState GameState
    {
        get => gameState;
        set => gameState = value;
    }

    /// <summary>
    /// Bussiness Logic Section
    /// </summary>

    /// <summary>
    /// Whether or not the game is finished.
    /// </summary>
    /// <returns>True if the game is finished, false otherwise.</returns>
    public bool IsFinished()
    {
        return GameState == GameState.Finished;
    }

    /// <summary>
    /// Whether or not the game is ready.
    /// </summary>
    /// <returns>True if the game is ready to be played, false otherwise.</returns>
    public bool IsReady()
    {
        return GameState == GameState.Ready;
    }

    /// <summary>
    /// Whether or not a game is currently in progress.
    /// </summary>
    /// <returns>True if the a game is in progress, false otherwise.</returns>
    public bool IsPlaying()
    {
        return GameState == GameState.Playing;
    }

    /// <summary>
    /// Whether or not the time ran out.
    /// </summary>
    /// <returns>True if the time ran out, false otherwise.</returns>
    public bool IsOutOfTime()
    {
        return Timer <= 0f;
    }

    /// <summary>
    /// Whether or not the goal has been reached.
    /// </summary>
    /// <returns>True if score has reached the goal, false otherwise.</returns>
    public bool IsGoalReached()
    {
        return Score >= Goal;
    }

    /// <summary>
    /// Whether or not the target color is ready to be changed.
    /// </summary>
    /// <returns>True if the target color is ready to be changed, false otherwise.</returns>
    public bool IsNextColor()
    {
        if (System.Math.Ceiling(Timer) % System.Math.Round(Delay) == 0f)
        {
            if (!DelayDebounce)
            {
                DelayDebounce = true;
                return true;
            }
            return false;
        }
        DelayDebounce = false;
        return false;
    }

    /// <summary>
    /// Resets the data to its default state.
    /// </summary>
    public void ResetData()
    {
        TargetColor = DefaultColor;
        delayDebounce = false;
        Score = 0;
        Timer = Timeout;
    }

    /// <summary>
    /// Returns a random color from Cthe olorPool field.
    /// </summary>
    /// <returns>Random color.</returns>
    public Color GetRandomColor()
    {
        return colorPool[Random.Range(0,colorPool.Length)];
    }

    /// <summary>
    /// Returns a list of random colors from the ColorPool field.
    /// </summary>
    /// <returns>List of random colors.</returns>
    public Color[] GetListOfRandomColors()
    {
        Color[] randomColors = new Color[colorPool.Length];

        for (int i = 0; i < colorPool.Length; i++)
        {
            randomColors[i] = GetRandomColor();
        }

        return randomColors;
    }
}
