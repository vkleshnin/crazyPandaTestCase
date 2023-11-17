using UnityEngine;

public class Gun : MonoBehaviour
{
    [Tooltip("Скорость снарядов.")] [SerializeField]
    private float speedProjectile = 1f;
    [Tooltip("Prefab снаряда.")] [SerializeField]
    private GameObject prefabProjectile;
    [Tooltip("Spawn point.")] [SerializeField]
    private Transform spawnPoint;

    public void Fire()
    {
        var projectile = Instantiate(prefabProjectile, spawnPoint.position, Quaternion.identity);
        var direction = transform.up;
        var rigidBody = projectile.GetComponent<Rigidbody2D>();
        rigidBody.velocity = direction * speedProjectile;
    }
}