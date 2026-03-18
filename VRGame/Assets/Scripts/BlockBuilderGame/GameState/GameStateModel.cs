using UnityEngine;
using UnityEngine.Assertions;

// TODO look at /VRGame/Assets/ScriptTemplate/Example.cs to see how to use this

/// <summary>
/// Model component of GameStateModel.
/// </summary>
public class GameStateModel : Model, IGameStateModel
{   
     // properties: puzzle 0 of 5 current puzzle
    [SerializeField] private int totalPuzzles;

    /// <inheritdoc/>
    public int TotalPuzzles
    {
        get
        {
            return totalPuzzles;
        }
        set
        {
            Assert.IsFalse(value < 0, "Total puzzles cannot be negative");
            Debug.Log("Setting totalPuzzles with" + value);
            totalPuzzles = value;
        }
    }

    // properties: puzzle 0 of 5 current puzzle
    [SerializeField] private int currentPuzzle;

    /// <inheritdoc/>
    public int CurrentPuzzle
    {
        get
        {
            return currentPuzzle;
        }
        set
        {
            Assert.IsFalse(value > TotalPuzzles, "There exist no puzzle greater than" + TotalPuzzles);
            Assert.IsFalse(value < 0, "Current puzzle selection cannot be a negative number");
            Debug.Log("Setting current puzzle to:" + value);
            currentPuzzle = value;
        }
    }


    // properties: number of blocks in the correct postion/ total blocks not in the right spot
    private int percentageOfCompletion;


    // number of blocks used in game
    private int numBlocksUsed;

    
    // number of blocks used in game
    private BlockModel[] winningBlock;

    // determines if puzzle is complete or not
    private bool puzzleCompleted;



    /// <inheritdoc/>
    public override void Init()
    {

    }


    /// <summary>
    /// Current level in play
    /// </summary>
    [SerializeField] private int currentLevel;
    public int CurrentLevel
    {
         get
        {
            return currentLevel;
        }
        set
        {
            Debug.Log("Setting current level to: " + value);
            currentLevel = value;
        }
    }

    
    /// <inheritdoc/>
    public void SetBrickPrefabsAvailable(int level, BlockShape[] shapesToInclude)
    {
        int totalBlockShapes = 0;
        switch(level){
            case 0:
                // to be implemented
                break;                
            case 1:
                // to be implemented
                break;
            case 2:
                // to be implemented
                break;
            case 3:
                // to be implemented
                break;
            case 4:
                // to be implemented
                break;
            case 5:
                // to be implemented
                break;
            default:
                UnityEngine.Debug.Assert(level >= 0 && level <= 5, "Level must be between 0 and 5");
                break;
        }

    }
}
