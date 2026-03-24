using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// BlockController interacts with the BlockModel to update the block's data and 
/// the BlockView to update the block's visuals
/// </summary>
public class BlockController : MonoBehaviour, IBlockController
{
    /// <summary>
    /// The model component that holds the block's data (type, position, rotation)
    /// </summary>
    [SerializeField] private IBlockModel model;

    /// <summary> 
    /// The view component that handles the block's visuals in the game
    /// </summary>
    [SerializeField] private IBlockView view;

    /// <summary>
    /// Awake method to initialize the Model and View components
    /// </summary>
    private void Awake()
    {
        // Get or add Model component
        model = GetComponent<IBlockModel>();
        if (model == null)
        {
            Debug.LogWarning("No IBlockModel found, adding BlockModel component");
            model = gameObject.AddComponent<BlockModel>();
        }
        else
        {
            Debug.Log("Cannot add BlockModel because one already exists on");
            Assert.IsNotNull("BlockController Model component is null on " + gameObject.name);
        }
        
        // Get or add View component
        view = GetComponent<IBlockView>();
        if (view == null)
        {
            Debug.LogWarning($"[BlockController] No IBlockView found, adding BlockView component");
            view = gameObject.AddComponent<BlockView>();
        }
        else
        {
            Debug.Log("Cannot add BlockView because one already exists");
            Assert.IsNotNull("BlockController View component is null on" + gameObject.name);
        }
        
        Assert.IsNotNull(model, "Model is null after initialization");
        Assert.IsNotNull(view, "View is null after initialization");
    }

    /// <inheritdoc/>
    public void Initialize(string blockType)
    {
        if (blockType == null)
        {
            Debug.LogError("Cannot initialize null BlockType");
            Assert.IsNotNull(blockType, "BlockType cannot be null in Initialize");
            return;
        }
        
        // Set the block type in the model and update the view
        model.BlockType = blockType;
        view.SetBlockType(blockType);
    }

    /// <inheritdoc/>
    public void UpdatePosition(Vector3 position)
    {
        if (position == null)
        {
            Debug.LogError("Cannot update position to null");
            Assert.IsNotNull("Position cannot be null in UpdatePosition");
            return;
        }
        Debug.Log("Updated position to " + position);
        Assert.IsNotNull(model, "BlockController Model is null in UpdatePosition");
        Assert.IsNotNull(view, "BlockController View is null in UpdatePosition");
        
        // Update the model's position and then update the view to transform the block in the scene
        model.Position = position;
        view.UpdateVisuals(position, model.Rotation);
    }

    /// <inheritdoc/>
    public void UpdateRotation(Quaternion rotation)
    {
        if (rotation == null)
        {
            Debug.LogError("Cannot update rotation to null");
            Assert.IsNotNull("Rotation cannot be null in UpdateRotation");
            return;
        }

        // Update the model's rotation and then update the view to transform the block in the scene
        model.Rotation = rotation;
        view.UpdateVisuals(model.Position, rotation);
    }
}