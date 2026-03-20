using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Model class for BlockSpawner
/// Contains all data related to brick spawning
/// </summary>
public class BlockSpawnerModel : MonoBehaviour, IBlockSpawnerModel
{

    /// <summary>
    /// Get as a List<BlockColour> These are all the possible shapes that exist in a puzzle
    /// </summary>
    private List<BlockColour> allBlockColours = Enum.GetValues(typeof(BlockColour))
                                .Cast<BlockColour>()
                                .ToList();


    /// <summary>
    ///Get as a List<Colors> These are all the colours to choose from for blocks
    /// </summary>
    private List<BlockShape> allBlockShapes = Enum.GetValues(typeof(BlockShape))
                                .Cast<BlockShape>()
                                .ToList();      


    /// <summary>
    /// Array of blockshapes to choose from for a specific puzzle
    /// </summary>
    private BlockShape[] blocksForPuzzle;

    /// <inheritdoc/>
    public BlockShape[] BlocksForPuzzle
    {
        get
        {
            return blocksForPuzzle;
        }
    }


    /// <summary>
    /// Transform indicating where bricks should spawn
    /// </summary>
    [SerializeField] private Transform spawnArea;

    /// <inheritdoc/>
    public Transform SpawnArea
    {
        get
        {
            return spawnArea;
        }
        set
        {
            Debug.Log("Setting SpawnArea to value to" + value);
            Assert.IsNotNull(value, "SpawnArea cannot be null");
            spawnArea = value;
        }
    }
    

    /// <summary>
    /// Height above the spawn area where bricks will be instantiated
    /// </summary>
    [SerializeField] private float spawnHeight;

    /// <inheritdoc/>
    public float SpawnHeight
    {
        get
        {
            return spawnHeight;
        }
        set
        {
            Debug.Log("Setting SpawnHeight from " + spawnHeight + " to " + value);
            spawnHeight = value;
        }
    }


    /// <summary>>
    /// Scale multiplier for spawned bricks
    /// </summary>
    [SerializeField] private float brickScale = 4.0f;
    
    /// <inheritdoc/>
    public float BrickScale
    {
        get
        {
            Debug.Log("Getting BrickScale: " + brickScale);
            return brickScale;
        }
        set
        {
            Assert.IsTrue(value > 0, "BrickScale must be greater than 0");
            Debug.Log("Setting BrickScale from " + brickScale + " to " + value);
            brickScale = value;
        }
    }



    /// <summary>
    /// Current index in the block cycle
    /// </summary>
    private int currentBlockShapeIndex;

    /// <inheritdoc/>
    public int CurrentBlockShapeIndex
    {
        get
        {
            return currentBlockShapeIndex;
        }
        set
        {
            Assert.IsTrue(value >= 0, "CurrentBlockIndex cannot be negative");
            Debug.Log("Setting CurrentBlockIndex from " + currentBlockShapeIndex + " to " + value);
            currentBlockShapeIndex = value;
        }
    }



    /// <summary>
    /// Current Selection of blocks representing an index in the brickPrefab array
    /// </summary>
    [SerializeField] private BlockShape currentBlockShapeSelected;
    public BlockShape CurrentBlockShapeSelected
    {
        get
        {
            return currentBlockShapeSelected;
        }
        set
        {
            Debug.Log("Setting currentBlockShapeSelected from " + currentBlockShapeSelected + " to " + value);
            currentBlockShapeSelected = value;
        }
    }

    

    /// <summary>
    /// Next in Selection of blocks representing an index in the brickPrefab array
    /// </summary>
    private BlockShape nextBlockShapeSelected;
    public BlockShape NextBlockShapeSelected{
        get
        {
            return nextBlockShapeSelected;
        }
    }


    /// <summary>
    /// Previous in Selection of blocks representing an index in the brickPrefab array
    /// </summary>
    private BlockShape prevBlockShapeSelected;
    public BlockShape PrevBlockShapeSelected{
        get
        {
            return prevBlockShapeSelected;
        }
    }



    /// <inheritdoc/>
    public void SelectNextBlockShape()
    {
        int count = blocksForPuzzle.Length; // Count of block shape types in puzzle

        Assert.IsTrue(currentBlockShapeIndex >= 0, "currentBlockShapeIndex must be greater than 0");
        Assert.IsTrue(currentBlockShapeIndex <= count, "currentBlockShapeIndex must be less or equal to blocks in puzzle");
        
        currentBlockShapeIndex ++;
        prevBlockShapeSelected = blocksForPuzzle[currentBlockShapeIndex - 1];

        if(currentBlockShapeIndex >= count )
        {
            currentBlockShapeIndex = 0;
            prevBlockShapeSelected = blocksForPuzzle[count - 1];
        }
        currentBlockShapeSelected = blocksForPuzzle[currentBlockShapeIndex];
        nextBlockShapeSelected = blocksForPuzzle[currentBlockShapeIndex + 1];
        if ((currentBlockShapeIndex + 1 ) == count )
        {
            nextBlockShapeSelected = blocksForPuzzle[0];
        }
        
        Debug.Log("Selecting Next block shape. \nPrevious: " + prevBlockShapeSelected +
        "Current: " + currentBlockShapeSelected + "Next: " + nextBlockShapeSelected);
    }


    /// <inheritdoc/>
    public void SelectPreviousBlockShape()
    {
        
        int count = blocksForPuzzle.Length; // Count of block shape types in puzzle

        Assert.IsTrue(currentBlockShapeIndex >= 0, "currentBlockShapeIndex must be greater than 0");
        Assert.IsTrue(currentBlockShapeIndex <= count, "currentBlockShapeIndex must be less or equal to blocks in puzzle");
        
        currentBlockShapeIndex --;
        nextBlockShapeSelected = blocksForPuzzle[currentBlockShapeIndex + 1];

        if(currentBlockShapeIndex < 0 )
        {
            currentBlockShapeIndex = count - 1;
            nextBlockShapeSelected = blocksForPuzzle[0];
        }
        currentBlockShapeSelected = blocksForPuzzle[currentBlockShapeIndex];
        prevBlockShapeSelected = blocksForPuzzle[currentBlockShapeIndex - 1];

        if (currentBlockShapeIndex == 0 )
        {
            prevBlockShapeSelected = blocksForPuzzle[count - 1];
        }
        
        Debug.Log("Selecting Previous block shape. \nPrevious: " + prevBlockShapeSelected +
        "Current: " + currentBlockShapeSelected + "Next: " + nextBlockShapeSelected);
    

    }


    /// <summary>
    /// Current index in the colour cycle
    /// </summary>
    [SerializeField] private int currentBlockColourIndex;

    /// <inheritdoc/>
    public int CurrentBlockColourIndex
    {
        get
        {
            return currentBlockColourIndex;
        }
        set
        {
            Assert.IsTrue(value >= 0, "CurrentColourIndex cannot be negative");
            Debug.Log("Setting CurrentColourIndex from " + currentBlockColourIndex + " to " + value);
            currentBlockColourIndex = value;
        }
    }


    /// <summary>
    /// Selected colour for blocks to spawn
    /// </summary>
    [SerializeField] BlockColour currentBlockColourSelected;

    public BlockColour CurrentBlockColourSelected
    {
        get
        {
            Debug.Log("Getting SelectedColour: " + currentBlockColourSelected);
            return currentBlockColourSelected;
        }
        set
        {
            Debug.Log("Setting SelectedColour from " + currentBlockColourSelected + " to " + value);
            currentBlockColourSelected = value;
        }
    }


    /// <summary>
    /// Next in Selection for colour blocks would spawn
    /// </summary>
    private BlockColour nextBlockColourSelected;
    public BlockColour NextBlockColourSelected{
        get
        {
            return nextBlockColourSelected;
        }
    }


    /// <summary>
    /// Previous in Selection for colour blocks would spawn
    /// </summary>
    private BlockColour prevBlockColourSelected;
    public BlockColour PrevBlockColourSelected{
        get
        {
            return prevBlockColourSelected;
        }
    }


    /// <inheritdoc/>
    public void SelectNextColour()
    {
        int count = allBlockColours.Count; // Count of all colours in puzzle

        Assert.IsTrue(currentBlockColourIndex >= 0, "currentBlockShapeIndex must be greater than 0");
        Assert.IsTrue(currentBlockColourIndex <= count, "currentBlockShapeIndex must be less or equal to allBlockColours.count");
        
        currentBlockColourIndex ++;

        if(currentBlockColourIndex >= count )
        {
            currentBlockColourIndex = 0;
            prevBlockColourSelected = allBlockColours[count - 1];
        } else
        {
            prevBlockColourSelected = allBlockColours[currentBlockColourIndex - 1];
        }

        nextBlockColourSelected = allBlockColours[currentBlockColourIndex + 1];
        if ((currentBlockColourIndex + 1 ) == count )
        {
            nextBlockColourSelected = allBlockColours[0];
        }        

        currentBlockColourSelected = allBlockColours[currentBlockColourIndex];

        Debug.Log("Selecting Next block Colour. \nPrevious: " + prevBlockColourSelected +
        "Current: " + currentBlockColourSelected + "Next: " + nextBlockColourSelected);

    }

    /// <inheritdoc/>
    public void SelectPreviousColour()
    {
                
        int count = allBlockColours.Count; // Count of block shape types in puzzle

        Assert.IsTrue(currentBlockColourIndex >= 0, "currentBlockColourIndex must be greater than 0");
        Assert.IsTrue(currentBlockColourIndex <= count, "currentBlockColoureIndex must be less or equal to count of allcolours");
        
        currentBlockColourIndex --;
        nextBlockColourSelected = allBlockColours[currentBlockColourIndex + 1];

        if(currentBlockShapeIndex < 0 )
        {
            currentBlockShapeIndex = count - 1;
            nextBlockColourSelected = allBlockColours[0];
        }
        currentBlockColourSelected = allBlockColours[currentBlockColourIndex];
        prevBlockColourSelected = allBlockColours[currentBlockShapeIndex - 1];

        if (currentBlockShapeIndex == 0 )
        {
            prevBlockColourSelected = allBlockColours[count - 1];
        }
        
        Debug.Log("Selecting Previous block colour. \nPrevious: " + prevBlockColourSelected +
        "Current: " + currentBlockColourSelected + "Next: " + nextBlockColourSelected);
    

    }


    /// <inheritdoc/>
    private void Initialize()
    {
        currentBlockColourSelected = BlockColour.white;
        currentBlockShapeIndex = 0;
        spawnHeight = 1.0f;
        brickScale = 4.0f;
    }


    /// <inheritdoc/>
    public void Init()
    {
        
    }
    
}