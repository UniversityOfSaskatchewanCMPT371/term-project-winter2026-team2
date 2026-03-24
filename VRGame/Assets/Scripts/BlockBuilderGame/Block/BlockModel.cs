using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model class for a block in the Block Builder Game 
/// It contains the block type, position, and rotation
/// </summary>
public class BlockModel : MonoBehaviour, IBlockModel
{
    /// <summary>
    /// The type of the block (e.g., bevel_lq_brick_1x1, bevel_lq_brick_1x2, etc.)
    /// </summary>
    [SerializeField] private string blockType;

    /// <inheritdoc/>
    public string BlockType
    {
        get
        {
            return blockType;
        }
        set
        {
            Assert.IsNotNull("BlockModel BlockType cannot be null");
            Debug.Log("BlockModel Setting BlockType from " + blockType + " to " + value);
            blockType = value;
        }
    }

    /// <summary>
    /// The position of the block in game
    /// </summary>
    [SerializeField] private Vector3 position;

    /// <inheritdoc/>
    public Vector3 Position
    {
        get
        {
            return position;
        }
        set
        {
            Debug.Log("BlockModel Setting Position from " + position + " to " + value);
            position = value;
        }
    }


    /// <summary>
    /// The rotation of the block in game
    /// </summary>
    [SerializeField] private Quaternion rotation;

    /// <inheritdoc/>
    public Quaternion Rotation
    {
        get
        {
            return rotation;
        }
        set
        {
            Debug.Log("BlockModel Setting Rotation from " + rotation.eulerAngles + " to " + value.eulerAngles);
            rotation = value;
        }
    }

    /// <summary>
    /// Initializes the BlockModel with default values
    /// </summary>
    public void initialization()
    {
        blockType = string.Empty;
        position = Vector3.zero;
        rotation = Quaternion.identity;
        Debug.Log("BlockModel initialized with default values");
        Assert.IsNotNull(blockType, "BlockType should not be null after initialization");
    }


}
