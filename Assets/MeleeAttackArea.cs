using UnityEngine;

public class MeleeAttackArea : MonoBehaviour
{
    public GameObject playerMovementGameObject;
    private int damage = 40;
    public int enemyHealth = 80;
    public Collider2D objectCollider;
    public Transform leftAttack;
    public Transform rightAttack;
    private Rigidbody2D rb; // Changed to Rigidbody2D
    public float knockbackForce = 10f;

    public GameObject enemyObject;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Changed to Rigidbody2D
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
            Debug.Log("is facing right " + isFacingRight);
            Debug.Log("intersecting" + IsIntersecting(isFacingRight ? rightAttack : leftAttack)); // Changed this line
            if ((isFacingRight && IsIntersecting(rightAttack)) || (!isFacingRight && IsIntersecting(leftAttack)))
            {
                Debug.Log("Damage Enemies");
                // Calculate knockback direction based on player's facing direction
                Vector2 knockbackDirection = isFacingRight ? Vector2.right : Vector2.left;
                // Apply knockback force
                rb.velocity = Vector2.zero; // Stop current movement
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse); // Apply knockback

                enemyHealth -= damage;
            }
        }
    }

    bool IsIntersecting(Transform attackPosition)
    {
        Bounds attackBounds = new Bounds(attackPosition.position, Vector3.one);
        return objectCollider.bounds.Intersects(attackBounds);
    }
}
