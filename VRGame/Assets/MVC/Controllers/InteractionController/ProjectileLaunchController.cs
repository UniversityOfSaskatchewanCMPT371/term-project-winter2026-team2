using UnityEngine;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Controller that handles projectile launching logic
    /// Extracted from LaunchProjectile.cs to separate controller logic from view
    /// </summary>
    public class ProjectileLaunchController : MonoBehaviour
    {
        [SerializeField, Tooltip("The projectile prefab that will be instantiated")]
        GameObject m_ProjectilePrefab;

        [SerializeField, Tooltip("The point where the projectile will be spawned")]
        Transform m_StartPoint;

        [SerializeField, Tooltip("The speed at which the projectile is launched")]
        float m_LaunchSpeed = 1.0f;

        [SerializeField, Tooltip("Whether to parent the projectile to a specific transform")]
        bool m_ParentProjectile = false;

        [SerializeField, Tooltip("Transform to parent projectiles to (if parenting is enabled)")]
        Transform m_ProjectileParent;

        /// <summary>
        /// Fire a projectile
        /// </summary>
        public void Fire()
        {
            if (m_ProjectilePrefab == null || m_StartPoint == null)
            {
                Debug.LogWarning("Cannot fire projectile: ProjectilePrefab or StartPoint is null", this);
                return;
            }

            var parent = m_ParentProjectile ? m_ProjectileParent : null;
            GameObject newProjectile = Instantiate(m_ProjectilePrefab, m_StartPoint.position, m_StartPoint.rotation, parent);

            if (newProjectile.TryGetComponent(out Rigidbody rigidBody))
            {
                ApplyForce(rigidBody);
            }
            else
            {
                Debug.LogWarning($"Projectile {newProjectile.name} does not have a Rigidbody component", this);
            }
        }

        /// <summary>
        /// Apply force to a projectile's rigidbody
        /// </summary>
        void ApplyForce(Rigidbody rigidBody)
        {
            Vector3 force = m_StartPoint.forward * m_LaunchSpeed;
            rigidBody.AddForce(force, ForceMode.Impulse);
        }

        /// <summary>
        /// Set the projectile prefab
        /// </summary>
        public void SetProjectilePrefab(GameObject prefab)
        {
            m_ProjectilePrefab = prefab;
        }

        /// <summary>
        /// Set the launch speed
        /// </summary>
        public void SetLaunchSpeed(float speed)
        {
            m_LaunchSpeed = speed;
        }

        /// <summary>
        /// Set the start point transform
        /// </summary>
        public void SetStartPoint(Transform startPoint)
        {
            m_StartPoint = startPoint;
        }

        /// <summary>
        /// Fire multiple projectiles in a spread pattern
        /// </summary>
        public void FireSpread(int count, float spreadAngle)
        {
            if (count <= 0)
                return;

            float angleStep = spreadAngle / (count - 1);
            float startAngle = -spreadAngle / 2f;

            for (int i = 0; i < count; i++)
            {
                float angle = startAngle + (angleStep * i);
                Quaternion rotation = m_StartPoint.rotation * Quaternion.Euler(0, angle, 0);
                
                var parent = m_ParentProjectile ? m_ProjectileParent : null;
                GameObject newProjectile = Instantiate(m_ProjectilePrefab, m_StartPoint.position, rotation, parent);

                if (newProjectile.TryGetComponent(out Rigidbody rigidBody))
                {
                    Vector3 force = newProjectile.transform.forward * m_LaunchSpeed;
                    rigidBody.AddForce(force, ForceMode.Impulse);
                }
            }
        }
    }
}
