using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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

    public void Awake()
    {
        Assert.IsNotNull(gridModel, "GridModel not assigned");
        Assert.IsNotNull(gridView, "GridView is not assigned");
        Assert.IsNotNull(gridController, "GridController not assigned");

        iGridModel = gridModel;
        iGridView = gridView;
        iGridController = gridController;
    }

    // Start is called before the first frame update
    void Start()
    {
        iGridController.Awake();

        iGridModel.Initialize(gridWidth, gridHeight, cellSize);
        iGridView.Initialize(gridWidth, gridHeight, cellSize);

        SetupTestPuzzle();

        Debug.Log("LogicGameManager initialized - Puzzle is ready");
    }

    public void SetupTestPuzzle()
    {
        CreateEndpoint(0, 0, Color.red, 0);
        CreateEndpoint(3, 3, Color.red, 0);

        CreateEndpoint(0, 3, Color.blue, 1);
        CreateEndpoint(3, 0, Color.blue, 1);

        iGridView.RenderEndpoints(iGridModel.Endpoints);

        Debug.Log($"Test puzzle set up with {iGridModel.Endpoints.Count} endpoints");
    }

    private void CreateEndpoint(int gridX, int gridY, Color color, int pairId)
    {
        Endpoint endpoint = new Endpoint
        {
            GridX = gridX,
            GridY = gridY,
            EndColor = color,
            PairId = pairId,
            IsConnected = false
        };

        iGridModel.AddEndpoint(endpoint);
    }

    public void ResetCurrentPuzzle()
    {
        iGridController.ResetGrid();
    }

    public bool IsPuzzleComplete()
    {
        return iGridController.CheckCompleted();
    }

    public IGridModel GetGridModel()
    {
        return iGridModel;
    }

    public IGridView GetGridView()
    {
        return iGridView;
    }

    public IGridController GetGridController()
    {
        return iGridController;
    }
}
