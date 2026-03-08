using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// The manager of the puzzle. Wires together the model, view, and controller.
/// Initializes all systems and manages a test puzzle 
/// 
/// TODO for ID4:
/// Puzzle needs to still be implemented in Unity
/// Couldn't figure out how to make it work inside of Unity
/// All logic should be good to go
/// </summary>
public class LogicGameManager : MonoBehaviour
{
    [SerializeField] private GridModel gridModel;
    [SerializeField] private GridView gridView;
    [SerializeField] private GridController gridController;
    [SerializeField] private int gridWidth = 4;
    [SerializeField] private int gridHeight = 4;
    [SerializeField] private float cellSize = 0.3f;

    private IGridModel iGridModel;
    private IGridView iGridView;
    private IGridController iGridController;

    /// <summary>
    /// Validates that the model, view, and controller are all assigned
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     - GridModel is assigned
    ///     - GridView is assigned
    ///     - GridController is assigned
    /// </pre-condition>
    /// <post-condition>
    ///     - Interfaces are references and initialized
    /// </post-condition>
    /// </remarks>
    public void Awake()
    {
        // Validates components are assigned
        Assert.IsNotNull(gridModel, "GridModel not assigned");
        Assert.IsNotNull(gridView, "GridView is not assigned");
        Assert.IsNotNull(gridController, "GridController not assigned");

        // Adding interfaces
        iGridModel = gridModel;
        iGridView = gridView;
        iGridController = gridController;
    }

    /// <summary>
    /// Initializes the puzzle system
    /// Called automatically after Awake()
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     - None
    /// <pre-condition>
    /// <post-condition>
    ///     - Game is ready to play
    ///     - Endpoints are visible
    /// </post-condition>
    /// </remarks>
    void Start()
    {
        iGridController.Awake();

        iGridModel.Initialize(gridWidth, gridHeight, cellSize);
        iGridView.Initialize(gridWidth, gridHeight, cellSize);

        SetupTestPuzzle();

        Debug.Log("LogicGameManager initialized - Puzzle is ready");
    }

    /// <summary>
    /// Creates a sample puzzle for testing
    /// Current puzzle is a 4x4 grid with 2 colored paired endpoints
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     - None
    /// </pre-condition>
    /// <post-condition>
    ///     - Endpoints are added to puzzle
    ///     - Puzzle is ready to solve
    /// </post-condition>
    /// </remarks>
    public void SetupTestPuzzle()
    {
        // Creating first endpoint pair
        CreateEndpoint(0, 0, Color.red, 0);
        CreateEndpoint(3, 3, Color.red, 0);

        // Creating second endpoint pair
        CreateEndpoint(0, 3, Color.blue, 1);
        CreateEndpoint(3, 0, Color.blue, 1);

        // Renders endpoints to the grid
        iGridView.RenderEndpoints(iGridModel.Endpoints);

        Debug.Log($"Test puzzle set up with {iGridModel.Endpoints.Count} endpoints");
    }

    /// <summary>
    /// Helper method that creates and adds a pair of endpoints to the puzzle
    /// </summary>
    /// <param name="gridX">X coordinate in grid</param>
    /// <param name="gridY">Y coordinate in grid</param>
    /// <param name="color">Color of the endpoints</param>
    /// <param name="pairId">Grouping of the endpoint pair created</param>
    /// <remarks>
    /// <pre-condition>
    ///     - None
    /// </pre-condition>
    /// <post-condition>
    ///     - Endpoints are created
    ///     - Endpoints are added to the list of valid endpoints
    /// </remarks>
    private void CreateEndpoint(int gridX, int gridY, Color color, int pairId)
    {
        // Creating the endpoint data structure
        Endpoint endpoint = new Endpoint
        {
            GridX = gridX,
            GridY = gridY,
            EndColor = color,
            PairId = pairId,
            IsConnected = false
        };

        // Adding endpoints to the model
        iGridModel.AddEndpoint(endpoint);
    }

    /// <summary>
    /// Resets the puzzle to the initial state
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     - None
    /// </pre-condition>
    /// <post-condition>
    ///     - All current pipes are removed from the puzzle
    ///     - Puzzle is ready to be solved as it was initialized
    /// </post-condition>
    /// </remarks>
    public void ResetCurrentPuzzle()
    {
        iGridController.ResetGrid();
    }

    /// <summary>
    /// Checks if the puzzle is currently solved
    /// Is solved once every endpoint is connected and grid is filled
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     - None
    /// </pre-condition>
    /// <post-condition>
    ///     - Verifies that all endpoints are connected
    ///     - If puzzle is complete, returns true
    /// </post-condition>
    /// </remarks>
    public bool IsPuzzleComplete()
    {
        return iGridController.CheckCompleted();
    }

    /// <summary>
    /// Gets the model interface
    /// </summary>
    public IGridModel GetGridModel()
    {
        return iGridModel;
    }

    /// <summary>
    /// Gets the view interface
    /// </summary>
    public IGridView GetGridView()
    {
        return iGridView;
    }

    /// <summary>
    /// Gets the controller interface
    /// </summary>
    public IGridController GetGridController()
    {
        return iGridController;
    }
}
