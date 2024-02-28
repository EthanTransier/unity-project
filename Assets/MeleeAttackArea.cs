using UnityEngine;
using System.Collections;

public class MeleeAttackArea : MonoBehaviour
{
    public GameObject playerMovementGameObject;
    private int damage = 40;
    public int enemyHealth = 80;
    public Collider2D objectCollider;
    public Transform leftAttack;
    public Transform rightAttack;
    private Rigidbody2D rb;
    public float knockbackForce = 10f;
    public float flashDuration = 0.1f;
    public Color flashColor = Color.red;
    private Color originalColor;
    private Renderer rend;
    public float knockbackDuration = 0.01f; // Adjust the duration as needed
    public GameObject enemyObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(enemyObject);
        }

        PlayerMovement playerMovement = playerMovementGameObject.GetComponent<PlayerMovement>();
        bool isFacingRight = playerMovement.isFacingRight;

        if (Input.GetButtonDown("Fire1"))
        {
            if ((isFacingRight && IsIntersecting(rightAttack)) || (!isFacingRight && IsIntersecting(leftAttack)))
            {
                enemyHealth -= damage;
                StartCoroutine(FlashCoroutine());
                // Calculate knockback direction based on player's facing direction
                Vector2 knockbackDirection = isFacingRight ? Vector2.right : Vector2.left;
                // Apply knockback force
                rb.velocity = Vector2.zero; // Stop current movement
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse); // Apply knockback
            }
        }
    }

    IEnumerator FlashCoroutine()
    {
        rend.material.color = flashColor;
        enemyObject.GetComponent<BotMovement>().ToggleMovement(false); // Disable movement during knockback
        // yield return new WaitForSeconds(flashDuration);
        rend.material.color = originalColor;
        yield return new WaitForSeconds(knockbackDuration); // Adjust knockback duration as needed
        enemyObject.GetComponent<BotMovement>().ToggleMovement(true); // Re-enable movement after knockback
    }


    bool IsIntersecting(Transform attackPosition)
    {
        Bounds attackBounds = new Bounds(attackPosition.position, Vector3.one);
        return objectCollider.bounds.Intersects(attackBounds);
    }
}
