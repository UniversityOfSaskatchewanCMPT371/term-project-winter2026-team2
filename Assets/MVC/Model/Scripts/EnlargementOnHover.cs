using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRScaleLinkedOnHover : MonoBehaviour
{
    public Transform[] linkedObjects;
    public float hoverScaleMultiplier = 1.1f;
    public float scaleSpeed = 10f;

    private Vector3[] normalScale;
    private Vector3[] biggerScale;

    void Awake()
    {
        //<summary>
        // Initialize the normal and bigger scales arrays
        //</summary>
        normalScale = new Vector3[linkedObjects.Length];
        biggerScale = new Vector3[linkedObjects.Length];

        //<summary>
        // Initialize the normal and bigger scales arrays using for loop
        //</summary>
        for (int i = 0; i < linkedObjects.Length; i++)
        {
            normalScale[i] = linkedObjects[i].localScale;
            biggerScale[i] = normalScale[i];
        }

        

    }

    void Update()
    {
        //<summary>
        // Update the scale of the linked objects using for loop
        //</summary>
        for (int i = 0; i < linkedObjects.Length; i++)
        {
            linkedObjects[i].localScale = Vector3.Lerp(
                linkedObjects[i].localScale,
                biggerScale[i],
                Time.deltaTime * scaleSpeed
            );
        }
    }
}