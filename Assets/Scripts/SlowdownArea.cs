using UnityEngine;

public class SlowdownArea : MonoBehaviour
{
    [Tooltip("Процент замедления.")] [Range(0.001f,1f)][SerializeField] 
    private float slowdownFactor = 0.1f;
    
    [Tooltip("Гравитация внутри зоны.")] [SerializeField]
    private bool disableGravity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Projectile")) return;
        
        //Debug.Log($"The object {other.gameObject.name} has entered the deceleration zone");
        var projectile = other.GetComponent<Projectile>();
        if (projectile is null) return;
        
        if (disableGravity) projectile.Rigidbody.gravityScale = 0;
        
        projectile.Rigidbody.velocity *= slowdownFactor;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Projectile")) return;
        
        //Debug.Log($"The object {other.gameObject.name} has left the deceleration zone.");
        var projectile = other.GetComponent<Projectile>();
        if (projectile is null) return;
        
        if (disableGravity) projectile.Rigidbody.gravityScale = 1;
        
        projectile.Rigidbody.velocity /= slowdownFactor;
    }
}