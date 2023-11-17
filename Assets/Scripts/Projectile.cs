using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    /// <summary>RigidBody2D снаряда.</summary>
    public Rigidbody2D Rigidbody { get; private set; }
    

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 2f);
    }
}
