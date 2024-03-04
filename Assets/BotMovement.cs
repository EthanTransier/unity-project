using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BotMovement : MonoBehaviour
{
    public Transform playerTransform;
    public LayerMask platformLayer;

    [SerializeField] private Transform leftWallCheck;
    [SerializeField] private Transform rightWallCheck;
    [SerializeField] private LayerMask wallLayer;
    public float moveSpeed = 2f;
    public float gravityScale = 1f;

    private Rigidbody2D rb;
    private bool canMove = true; // Flag to control movement

    private bool moveRight = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canMove)
        {
            if (Mathf.Abs(playerTransform.position.y - transform.position.y) < 5 && Mathf.Abs(playerTransform.position.x - transform.position.x) < 25)
            {
                MoveToPlayerHorizontal();
            }
            else
            {
                Idle();
            }
        }
    }

    void MoveToPlayerHorizontal()
    {
        float direction = Mathf.Sign(playerTransform.position.x - transform.position.x);
        if (direction > 0)
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }
        rb.velocity = new Vector2(direction * (moveSpeed + 6f), rb.velocity.y);
    }
    void Idle()
    {
        if (IsLeftWalled())
        {
            moveRight = true;
        }
        else if (IsRightWalled())
        {
            moveRight = false;
        }
        if (moveRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-1 * moveSpeed, rb.velocity.y);
        }

    }
    private bool IsLeftWalled()
    {
        return Physics2D.OverlapCircle(leftWallCheck.position, 0.2f, wallLayer);
    }
    private bool IsRightWalled()
    {
        return Physics2D.OverlapCircle(rightWallCheck.position, 0.2f, wallLayer);
    }

    public void ToggleMovement(bool enable)
    {
        canMove = enable;
    }
}
