using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BotMovement : MonoBehaviour
{
    public Transform playerTransform;
    public LayerMask platformLayer;
    public float moveSpeed = 2f;
    public float gravityScale = 1f;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isGrounded)
        {
            if (IsPlayerAbove())
            {
                MoveToPlayerHorizontal();
            }
            else
            {
                JumpToPlayer();
            }
        }
    }

    bool IsPlayerAbove()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 2f, platformLayer);
        if (hit.collider != null)
        {
            // Check if the player is directly above the enemy on the same platform
            float playerY = playerTransform.position.y;
            float enemyY = transform.position.y;
            return playerY > enemyY && Mathf.Abs(playerTransform.position.x - transform.position.x) < 0.5f;
        }
        return false;
    }

    void MoveToPlayerHorizontal()
    {
        float direction = Mathf.Sign(playerTransform.position.x - transform.position.x);
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    void JumpToPlayer()
    {
        float heightDifference = playerTransform.position.y - transform.position.y;
        float jumpForce = CalculateJumpForce(heightDifference);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    float CalculateJumpForce(float heightDifference)
    {
        // Adjust this value to control the jump strength
        float jumpForceMultiplier = 5f;
        return Mathf.Sqrt(2f * Mathf.Abs(Physics2D.gravity.y) * heightDifference) * jumpForceMultiplier;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}
