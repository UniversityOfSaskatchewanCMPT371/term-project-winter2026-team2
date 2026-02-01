using UnityEngine;
using UnityEngine.Events;

[ RequireComponent(typeof(Collision)) ]
[ RequireComponent(typeof(Rigidbody)) ]
public class HammerView : MonoBehaviour
{
    public UnityEvent<Collision> OnHammerHit;

    private void OnCollisionEnter(Collision other)
    {
        OnHammerHit?.Invoke(other);
    }
}