using UnityEngine;

[ RequireComponent(typeof(WhackAColorData)) ]
[ RequireComponent(typeof(WhackAColorView)) ]

/// <summary>
/// Controller layer for WhackAColor minigame.
/// </summary>
public class WhackAColorLogic : MonoBehaviour
{
    public WhackAColorData whackAColorData;                 // Reference to Model layer
    public WhackAColorView whackAColorView;                 // Reference to View layer

    /// <summary>
    /// Handles changing to next target color, and randomizes
    /// the color of the cubes.
    /// </summary>
    private void NextTargetColor()
    {
        // get lists of random colors to be used

        Color[] colorPool = whackAColorData.GetListOfRandomColors();

        // Pick a random color as the next target

        Color targetColor = colorPool[Random.Range(0, colorPool.Length)];
        whackAColorData.TargetColor = targetColor;

        whackAColorView.ChangeTargetColor(targetColor);

        // Randomize the colors of the cube
        whackAColorView.RandomizeCubeColors(colorPool);
    }

    /// <summary>
    /// Starts the minigame.
    /// </summary>
    public void StartGame()
    {
        // throw exception here

        if (!whackAColorData.IsReady() || whackAColorData.IsPlaying()) return;

        whackAColorData.ResetData();

        NextTargetColor();

        whackAColorData.GameState = GameState.Playing;
    }

    /// <summary>
    /// Finishes the minigame.
    /// </summary>
    public void FinishGame()
    {
        whackAColorView.ChangeAllCubeColors(whackAColorData.defaultColor);
        whackAColorView.DisplayResults(whackAColorData.IsGoalReached());
        whackAColorView.Invoke("DisplayTitle", 3);
        whackAColorData.ResetData();
        whackAColorData.GameState = GameState.Ready;
    }

    /// <summary>
    /// Verifys that the color hit was the target color.
    /// </summary>
    /// <param name="color">Color that was hit.</param>
    public void VerifyColorHit(Color color)
    {
        if (!whackAColorData.IsPlaying()) return;

        if (color == whackAColorData.TargetColor)
        {
            whackAColorData.Score++;
            whackAColorView.ChangeAllCubeColors(Color.green);
        } else
        {
            whackAColorView.ChangeAllCubeColors(Color.red);
        }
    }

    /// <summary>
    /// Update the status of the game each frame.
    /// </summary>
    private void Update()
    {
        // Don't update when the game status is ready
        if (whackAColorData.IsReady()) 
            return;
        else if (whackAColorData.IsPlaying())
        {   
            // Decrement time
            whackAColorData.Timer -= Time.deltaTime;

            // Update the view about the score and timer
            whackAColorView.DisplayStatus(whackAColorData.Score, whackAColorData.Goal, whackAColorData.Timer);

            // Check if the goal has been reached
            // Check if time ran out
            // Otherwise change color when possible
            if (whackAColorData.IsGoalReached())
            {
                whackAColorData.GameState = GameState.Finished;
            } else if (whackAColorData.IsOutOfTime())
            {
                whackAColorData.GameState = GameState.Finished;
            } else if (whackAColorData.IsNextColor())
            {
                NextTargetColor();
            }
        } else if (whackAColorData.IsFinished())
        {
            FinishGame();
        }
    }
}
