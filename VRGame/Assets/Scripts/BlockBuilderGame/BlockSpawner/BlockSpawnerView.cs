using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// View class for BlockSpawner
/// Handles visual representation and instantiation of bricks
/// </summary>
public class BlockSpawnerView : MonoBehaviour, IBlockSpawnerView
{
    /// <inheritdoc/>
    public GameObject InstantiateBrick(GameObject prefab, Vector3 position, Quaternion rotation, float scale)
    {
        if (prefab == null)
        {
            Debug.LogError("Cannot instantiate brick. Prefab is null");
            return null;
        }
        Assert.IsNotNull(prefab, "Prefab cannot be null");
        

        if (scale <= 0)
        {
            Debug.LogError("Scale must be greater than 0");
            return null;
        }
        Assert.IsTrue(scale > 0, "Scale must be greater than 0");

        // Instantiate the brick and set its scale
        GameObject spawnedBrick = Instantiate(prefab, position, rotation);
        spawnedBrick.transform.localScale = Vector3.one * scale;

        return spawnedBrick;
    }

    /// <inheritdoc/>
    public void ConfigureBrickVisuals(GameObject brick)
    {
        if (brick == null)
        {
            Debug.LogError("Cannot configure visuals for brick. Brick is null");
            return;
        }
        Assert.IsNotNull(brick, "Brick GameObject cannot be null");

        // Enable all MeshRenderers and assign random colors if material is missing
        MeshRenderer[] renderers = brick.GetComponentsInChildren<MeshRenderer>();
        if (renderers.Length <= 0)
        {
            Debug.LogWarning("No MeshRenderer found on brick " + brick.name);
            return;
        }
        else
        {
            // Enable all renderers and assign random colors if material is missing
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.enabled = true;
                if (renderer.sharedMaterial == null)
                {
                    renderer.material = new Material(Shader.Find("Standard"));
                    renderer.material.color = GetRandomBrickColor();
                }
            }
        }

        // Add MeshColliders to all child objects
        MeshFilter[] meshFilters = brick.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter meshFilter in meshFilters)
        {
            if (meshFilter.gameObject.GetComponent<Collider>() == null)
            {
                MeshCollider meshCollider = meshFilter.gameObject.AddComponent<MeshCollider>();
                meshCollider.convex = true;
            }

        }

        // Add Rigidbody to the root object if it doesn't have one
        if (brick.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = brick.AddComponent<Rigidbody>();
            rb.mass = 0.5f;
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        else
        {
            Rigidbody rb = brick.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
        }

    }

    /// <inheritdoc/>
    private Color GetRandomBrickColor()
    {
        Color[] brickColors = new Color[]
        {
            Color.red,
            Color.blue,
            Color.green,
            Color.yellow,
            new Color(1f, 0.5f, 0f), // Orange
            new Color(0.5f, 0f, 1f)  // Purple
        };
        Color selectedColor = brickColors[Random.Range(0, brickColors.Length)];
        return selectedColor;
    }

}
