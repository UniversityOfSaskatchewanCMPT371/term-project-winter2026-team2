using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Controller class for SpawnButton
/// Manages the logic for handling button press events to spawn bricks
/// </summary>
public class SpawnButtonController : Controller<IBlockSpawnerModel, ISpawnButtonView>, // TODO reminder to switch the generics to the ones you've implemented
    ISpawnButtonController
{
    /// <summary>
    /// Reference to the BlockSpawnerController 
    /// </summary>
    [SerializeField] private BlockSpawnerController blockSpawnerController;

    /// <summary>
    /// Boolean flag to enable mouse click 
    /// </summary>
    [SerializeField] private bool enableMouseClick = true;

    /// <summary>
    /// Instance of the Model and View components
    /// </summary>
    private ISpawnButtonModel model;
    private ISpawnButtonView view;


    /// <summary>
    /// Awake method to initialize Model and View components
    /// </summary>
    private void Awake()
    {
        // Get or add Model component
        model = GetComponent<ISpawnButtonModel>();
        if (model == null)
        {
            model = gameObject.AddComponent<SpawnButtonModel>();
        }

        // Get or add View component
        view = GetComponent<ISpawnButtonView>();
        if (view == null)
        {
            Debug.Log("No ISpawnButtonView found, adding SpawnButtonView component");
            view = gameObject.AddComponent<SpawnButtonView>();
        }

        Assert.IsNotNull(model, "Model is null after initialization!");
        Assert.IsNotNull(view, "View is null after initialization!");
        
        Debug.Log("SpawnButtonController Model and View initialized successfully");
    }


    /// <summary>
    /// Start method to subscribe to button press events
    /// </summary>
    private void Start()
    {
        // Verify BlockSpawnerController reference
        if (blockSpawnerController == null)
        {
            Debug.LogError("BlockSpawnerController reference is not set in inspector");
        }
    }

    private void OnEnable()
    {
        if (view != null)
        {
            view.Subscribe(OnButtonPressed);
            Debug.Log("SpawnButtonController successfully subscribed to button events");
        }
    }

    private void OnDisable()
    {
        if (view != null)
        {
            view.Unsubscribe(OnButtonPressed);
        }
    }

    private void Update()
    {
        // Check for mouse click
        if (enableMouseClick && Input.GetMouseButtonDown(0))
        {
            // Raycast from mouse position to check if button was clicked
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        Debug.Log("SpawnButtonController Button clicked with mouse!");
                        HandleButtonPress();
                    }
                }
            }
        }
    }

    /// <inheritdoc/>
    public void Initialize(BlockSpawnerController spawnerController)
    {
        Assert.IsNotNull(spawnerController, "BlockSpawnerController cannot be null on Initialize");
        blockSpawnerController = spawnerController;
    }

    /// <inheritdoc/>
    public void HandleButtonPress()
    {
        if (blockSpawnerController != null)
        {
            blockSpawnerController.SpawnNextBrick();
        }
    }

    /// <inheritdoc/>
    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        Debug.Log("SpawnButtonController Button pressed with VR controller!");
        HandleButtonPress();
    }
    
}
