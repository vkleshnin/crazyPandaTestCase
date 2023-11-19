using UnityEngine;

public class SlowdownArea : MonoBehaviour
{
    [Tooltip("Процент замедления.")] [Range(0.001f,1f)][SerializeField] 
    private float slowdownFactor = 0.1f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Projectile")) return;
        
        //Debug.Log($"The object {other.gameObject.name} has entered the deceleration zone");
        var projectileRb = other.GetComponent<Rigidbody2D>();
        if (projectileRb == null) return;

        projectileRb.velocity *= slowdownFactor;
        projectileRb.gravityScale *= (slowdownFactor * slowdownFactor);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Projectile")) return;
        
        //Debug.Log($"The object {other.gameObject.name} has left the deceleration zone.");
        var projectileRb = other.GetComponent<Rigidbody2D>();
        if (projectileRb == null) return;
        
        projectileRb.velocity /= slowdownFactor;
        projectileRb.gravityScale = 1f;
    }
}