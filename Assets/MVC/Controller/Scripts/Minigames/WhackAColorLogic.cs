using UnityEngine;

[ RequireComponent(typeof(WhackAColorData)) ]
[ RequireComponent(typeof(WhackAColorView)) ]
public class WhackAColorLogic : MonoBehaviour
{
    public WhackAColorData whackAColorData;
    public WhackAColorView whackAColorView;

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

    public void StartGame()
    {
        // throw exception here

        if (!whackAColorData.IsReady() || whackAColorData.IsPlaying()) return;

        whackAColorData.ResetData();

        NextTargetColor();

        whackAColorData.GameState = GameState.Playing;
    }

    public void FinishGame()
    {
        whackAColorView.ChangeAllCubeColors(whackAColorData.defaultColor);
        whackAColorView.MinigameFinished(whackAColorData.IsGoalReached());
        whackAColorData.ResetData();
        whackAColorData.GameState = GameState.Ready;
    }

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
            whackAColorView.OnUpdate(whackAColorData.Score, whackAColorData.Goal, whackAColorData.Timer);

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
