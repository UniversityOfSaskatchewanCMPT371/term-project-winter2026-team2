using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class GridView : MonoBehaviour, IGridView
{

    /// <inheritdoc/>
    public void Initialize(int width, int height, float cellSize)
    {
        
    }

    /// <inheritdoc/>
    public void RenderPipe(int x, int y, Color color, Vector3 worldPosition)
    {
        
    }

    /// <inheritdoc/>
    public void RenderEndpoints(List<Endpoint> endpoints)
    {
        
    }

    /// <inheritdoc/>
    public void ClearAllPipes()
    {
        
    }

    /// <inheritdoc/>
    public void ShowCompletionEffect()
    {
        
    }

    /// <inheritdoc/>
    public void HighlightCell(Vector3 position)
    {
        
    }

    /// <inheritdoc/>
    public void ClearHighlight()
    {
        
    }

}