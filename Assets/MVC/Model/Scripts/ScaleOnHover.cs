using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRScaleLinkedOnHover : MonoBehaviour
{
    public Transform[] linkedObjects;
    public float hoverScaleMultiplier = 1.25f;
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

        //<summary>
        // A hover-entered event listener to scale the linked objects to bigger scale
        //</summary>
        var modelInteractable = GetComponent<XRBaseInteractable>();
        modelInteractable.hoverEntered.AddListener(_ =>
        {
            for (int i = 0; i < linkedObjects.Length; i++)
                biggerScale[i] = normalScale[i] * hoverScaleMultiplier;
        });

        //<summary>
        // A hover-exited event listener to scale the linked objects back to normal
        //</summary>
        modelInteractable.hoverExited.AddListener(_ =>
        {
            for (int i = 0; i < linkedObjects.Length; i++)
                biggerScale[i] = normalScale[i];
        });
    }

    void Update()
    {
        //<summary>
        // Update the scale of the linked objects using for loop
        // Transition smoothly between normal and bigger scales with scaleSpeed
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