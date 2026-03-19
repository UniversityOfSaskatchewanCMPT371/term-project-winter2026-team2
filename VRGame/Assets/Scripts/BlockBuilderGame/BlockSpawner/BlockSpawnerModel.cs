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
    // Get as a List<BlockColour>
    private List<BlockColour> AllColours = Enum.GetValues(typeof(BlockColour))
                                .Cast<BlockColour>()
                                .ToList();

    // Get as a List<Colors>
    private List<BlockShape> AllBlockShapes = Enum.GetValues(typeof(BlockShape))
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
            Assert.IsTrue(value != null, "CurrentBrickIndex cannot be null");
            Debug.Log("Setting currentBlockShapeSelected from " + currentBlockShapeSelected + " to " + value);
            currentBlockShapeSelected = value;
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


    

    /// <inheritdoc/>
    public void SelectNextBlockShape()
    {
        // to be implemented later

    }

    /// <inheritdoc/>
    public void SelectPreviousBlockShape()
    {
        // to be implemented later

    }

    /// <inheritdoc/>
    public void SelectNextColour()
    {
        // to be implemented later

    }

    /// <inheritdoc/>
    public void SelectPreviousColour()
    {
        // to be implemented later

    }


    /// <inheritdoc/>

    public void SetBlocksForPuzzle(int level, BlockShape requiredBlockShapes)
    {
        /*
        Assert.IsNotNull(value, "blocksAvailable cannot be null");
        Debug.Log("Setting BlocksAvailable array with " + value.Length + " elements");
        blocksAvailable = value;
        */
        
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
    /// Current index in the colour cycle
    /// </summary>
    [SerializeField] private int currentColourIndex;

    /// <inheritdoc/>
    public int CurrentColourIndex
    {
        get
        {
            return currentColourIndex;
        }
        set
        {
            Assert.IsTrue(value >= 0, "CurrentColourIndex cannot be negative");
            Debug.Log("Setting CurrentColourIndex from " + currentColourIndex + " to " + value);
            currentColourIndex = value;
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





    /// <inheritdoc/>
    private void Initialize()
    {
        currentBlockColourSelected = BlockColour.white;
        currentBlockShapeIndex = 0;
        spawnHeight = 1.0f;
        brickScale = 4.0f;
    }

    
}