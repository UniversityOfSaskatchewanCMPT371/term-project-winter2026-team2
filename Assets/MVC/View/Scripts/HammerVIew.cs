using UnityEngine;
using UnityEngine.Events;

[ RequireComponent(typeof(Collision)) ]
[ RequireComponent(typeof(Rigidbody)) ]

/// <summary>
/// View layer for hammer prefab.
/// </summary>
public class HammerView : MonoBehaviour
{
    public UnityEvent<Collision> OnHammerHit;

    private void OnCollisionEnter(Collision other)
    {
        OnHammerHit?.Invoke(other);
    }
}