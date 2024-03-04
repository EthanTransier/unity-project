using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the projectile collides with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Destroy the projectile when it hits the ground
            Destroy(gameObject);
        }
    }
}
