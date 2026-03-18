using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model class for a block in the Block Builder Game 
/// It contains the block type, position, and rotation
/// </summary>
public class BlockModel : MonoBehaviour, IBlockModel
{
    /// <summary>
    /// The shape of the block (e.g., bevel_lq_brick_1x1, bevel_lq_brick_1x2, etc.)
    /// </summary>
    [SerializeField] private BlockShape shape;

    /// <inheritdoc/>
    public BlockShape Shape
    {
        get
        {
            return Shape;
        }
        set
        {
            Assert.IsNotNull("BlockModel: BlockShape cannot be null");
            Debug.Log("BlockModel Setting BlockShape from " + shape + " to " + value);
            shape = value;
        }
    }

    /// <summary>
    /// The colour of the block (e.g., red, blue, green etc.)
    /// </summary>
    [SerializeField] private BlockColour colour;

    /// <inheritdoc/>
    public BlockColour Colour
    {
        get
        {
            return colour;
        }
        set
        {
            Debug.Log("BlockModel Setting BlockColour from " + colour + " to " + value);
            colour = value;
        }
    }

    /// <summary>
    /// The current postion of the block.
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
    /// The target postion of the block.
    /// </summary>
    [SerializeField] private Vector3 targetPosition;

    /// <inheritdoc/>
    public Vector3 TargetPosition
    {
        get
        {
            return targetPosition;
        }
        set
        {
            Debug.Log("BlockModel Setting TargetPosition from " + targetPosition + " to " + value);
            targetPosition = value;
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
    public void initialization(BlockShape s, BlockColour c)
    {
        Shape = s; 
        Colour = c;
        Position = Vector3.zero;
        rotation = Quaternion.identity;
        Debug.Log("BlockModel initialized with default values");
    }


}
