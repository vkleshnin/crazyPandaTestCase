using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownArea : MonoBehaviour
{
    [Tooltip("Процент замедления.")] [Range(0.001f,1f)][SerializeField] 
    private float slowdownFactor = 0.1f;
    
    [Tooltip("Вкл/выкл гравитацию.")][SerializeField]
    private bool disableGravity;
    
    [Tooltip("Замедлить гравитацию.")][SerializeField]
    private bool slowdownGravity = true;
    
    private readonly Dictionary<Rigidbody2D, float> _enteredProjectiles = new ();

    private void Update()
    {
        List<Rigidbody2D> keysToRemove = new List<Rigidbody2D>();

        foreach (var pair in _enteredProjectiles)
        {
            var rb = pair.Key;
            var originalSpeed = pair.Value;
            if (rb != null)
            {
                var normalisedVelocity = rb.velocity.normalized;
                var targetSpeed = originalSpeed * slowdownFactor;
                var newSpeed = Mathf.Lerp(rb.velocity.magnitude, targetSpeed, 0.01f);
                rb.velocity = normalisedVelocity * newSpeed;
            }
            else
            {
                keysToRemove.Add(rb);
            }
        }

        foreach (var key in keysToRemove)
        {
            _enteredProjectiles.Remove(key);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Projectile")) return;
        
        //Debug.Log($"The object {other.gameObject.name} has entered the deceleration zone");
        var projectileRigidbody = other.GetComponent<Rigidbody2D>();
        if (projectileRigidbody == null || _enteredProjectiles.ContainsKey(projectileRigidbody)) return;
        
        _enteredProjectiles.Add(projectileRigidbody, projectileRigidbody.velocity.magnitude);
        
        if (disableGravity)
            projectileRigidbody.gravityScale = 0;
        else if (slowdownGravity)
            projectileRigidbody.gravityScale *= slowdownFactor;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Projectile")) return;
        
        //Debug.Log($"The object {other.gameObject.name} has left the deceleration zone.");
        var projectileRigidbody = other.GetComponent<Rigidbody2D>();
        if (projectileRigidbody == null || !_enteredProjectiles.ContainsKey(projectileRigidbody)) return;
        
        StartCoroutine(RestoreSpeed(projectileRigidbody, _enteredProjectiles[projectileRigidbody]));
        _enteredProjectiles.Remove(projectileRigidbody);
        
        if (disableGravity)
            projectileRigidbody.gravityScale = 1;
        else if (slowdownGravity)
            projectileRigidbody.gravityScale /= slowdownFactor;
    }
    
    private IEnumerator RestoreSpeed(Rigidbody2D rigidbody2d, float originalSpeed)
    {
        while (rigidbody2d is not null && rigidbody2d.velocity.magnitude < originalSpeed)
        {
            var newSpeed = Mathf.Lerp(rigidbody2d.velocity.magnitude, originalSpeed, 0.1f);
            rigidbody2d.velocity = rigidbody2d.velocity.normalized * newSpeed;
            yield return new WaitForEndOfFrame();
        }

        if (rigidbody2d != null) _enteredProjectiles.Remove(rigidbody2d);
    }
}