using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject, 2f);
    }
}
