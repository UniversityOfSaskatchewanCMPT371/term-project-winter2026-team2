using UnityEngine;
using UnityEngine.Assertions;

[ RequireComponent(typeof(DoorLogic)) ]
[ RequireComponent(typeof(Collider)) ]
public class DoorView : MonoBehaviour
{
    public DoorLogic doorLogic;         // Reference to the doorLogic attached to this object

    /// <summary>
    /// 
    /// When this script is loaded, it will process sanity checks.
    /// 
    /// </summary>
    private void Awake()
    {
        Assert.IsNotNull(doorLogic, "Field DoorLogic cannot be null.");
    }

    /// <summary>
    /// 
    /// Triggers when another collider enters this object's collider.
    /// 
    /// </summary>
    /// <param name="other">The collider that entered this object's collider.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("MainCamera")) return;

        doorLogic.OnPlayerEnter(other.GetComponentInParent<PlayerData>().gameObject);
    }
}
