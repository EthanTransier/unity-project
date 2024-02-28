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
            Debug.Log(isFacingRight);
            Debug.Log(IsIntersecting(rightAttack));
            if ((isFacingRight && IsIntersecting(rightAttack)) || (!isFacingRight && IsIntersecting(leftAttack)))
            {
                Debug.Log("attack");
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
        yield return new WaitForSeconds(knockbackDuration);
        rend.material.color = originalColor;
        // Adjust knockback duration as needed
        enemyObject.GetComponent<BotMovement>().ToggleMovement(true); // Re-enable movement after knockback
    }


    bool IsIntersecting(Transform attackPosition)
    {
        // Assuming the attack position is a point (like the position of the attack)
        Vector3 attackPoint = attackPosition.position;
        Debug.Log("attack position" + attackPosition.position);
        // Calculate a small bounds around the attack point
        Bounds attackBounds = new Bounds(attackPoint, new Vector3(1f, 1f, 1f));
        Debug.Log("attack bounds " + attackBounds);
        // Check for intersection with the enemy collider bounds
        return attackBounds.Intersects(objectCollider.bounds);
    }
}
