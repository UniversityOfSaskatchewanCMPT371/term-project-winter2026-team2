using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System.IO;

public class GridView : MonoBehaviour, IGridView
{
    private Transform pipeContainer;
    private Dictionary<string, GameObject> renderedPipes = new Dictionary<string, GameObject>();
    private PipeManager pipeManager;
    private int gridWidth;
    private int gridHeight;
    private float cellSize;


    /// <inheritdoc/>
    public void Initialize(int width, int height, float cellSize)
    {
        Assert.IsTrue(width > 0, "Width must be > 0");
        Assert.IsTrue(height > 0, "Height must be > 0");
        Assert.IsTrue(cellSize > 0, "Cell size must be > 0");

        this.gridWidth = width;
        this.gridHeight = height;
        this.cellSize = cellSize;

        pipeContainer = new GameObject("PipeContainer").transform;
        pipeContainer.parent = transform;
        pipeContainer.localPosition = Vector3.zero;

        pipeManager = GetComponent<PipeManager>();
        if (pipeManager == null)
        {
            pipeManager = gameObject.AddComponent<PipeManager>();
        }

        Debug.Log($"GridView initialized for {width}x{height} grid");
    }

    /// <inheritdoc/>
    public void RenderPipe(int gridX, int gridY, Color color, Vector3 worldPosition, Panel panel)
    {
        Assert.IsNotNull(pipeContainer, "PipeContainer must be initialized");
        Assert.IsNotNull(pipeManager, "PipeManager must be initialized");

        string key = $"Pipe_{gridX}_{gridY}";

        GameObject pipeGO = new GameObject(key);
        pipeGO.transform.parent = pipeContainer;
        pipeGO.transform.position = worldPosition;
        pipeGO.transform.localScale = new Vector3(cellSize, cellSize, 1f);

        MeshFilter meshFilter = pipeGO.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = pipeGO.AddComponent<MeshRenderer>();

        Mesh quadMesh = CreateQuadMesh();
        meshFilter.mesh = quadMesh;

        Texture2D texture = pipeManager.GetPipeTexture(panel);
        Material mat = new Material(Shader.Find("Standard"));
        if (texture != null)
        {
            mat.mainTexture = texture;
        }
        else
        {
            mat.color = color;
        }

        meshRenderer.material = mat;

        renderedPipes[key] = pipeGO;

        Assert.IsTrue(renderedPipes.ContainsKey(key), "Pipe should be stored in dictionary");
    }

    /// <inheritdoc/>
    public void RenderEndpoints(List<Endpoint> endpoints)
    {
        Assert.IsNotNull(endpoints, "Endpoints list cannot be null");

        foreach (var endpoint in endpoints)
        {
            GameObject endpointGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            endpointGO.name = $"Endpoint_{endpoint.GridX}_{endpoint.GridY}";
            endpointGO.transform.parent = pipeContainer;
            endpointGO.transform.localScale = Vector3.one * cellSize;

            Vector3 pos = new Vector3(
                endpoint.GridX * cellSize - (gridWidth * cellSize) / 2f,
                endpoint.GridY * cellSize - (gridHeight * cellSize) / 2f,
                -0.1f
            );
            endpointGO.transform.position = pos;

            Renderer renderer = endpointGO.GetComponent<Renderer>();
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = endpoint.EndColor;
            renderer.material = mat;

            Destroy(endpointGO.GetComponent<Collider>());
        }
    }

    /// <inheritdoc/>
    public void ClearAllPipes()
    {
        Assert.IsNotNull(pipeContainer, "PipeContainer must be initialized");

        foreach (var pipe in renderedPipes.Values)
        {
            Destroy(pipe);
        }
        renderedPipes.Clear();

        Assert.IsTrue(renderedPipes.Count == 0, "All pipes should be cleared");
    }

    /// <inheritdoc/>
    public void ShowCompletionEffect()
    {
        Debug.Log("Puzzle Complete!");
    }

    /// <inheritdoc/>
    public void HighlightCell(Vector3 position)
    {
        
    }

    /// <inheritdoc/>
    public void ClearHighlight()
    {
        
    }

    private Mesh CreateQuadMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-0.5f, -0.5f, 0f),
            new Vector3(0.5f, -0.5f, 0f),
            new Vector3(0.5f, 0.5f, 0f),
            new Vector3(-0.5f, 0.5f, 0f)
        };

        int[] triangles = new int[]
        {
            0, 2, 1,
            0, 3, 2
        };

        Vector2[] uv = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        return mesh;
    }

}