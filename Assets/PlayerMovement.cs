using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // For animation
    public Animator animator;

    private float horizontal;
    private float speed = 20f;
    private float jumpingPower = 28f;
    private int jumps = 2;
    public bool isFacingRight = true;
    private bool canDash = true;
    private bool isDashing;
    // private float dashingPower = 8f;
    private float dashingPower = 50f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform leftWallCheck;
    [SerializeField] private Transform rightWallCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    void Update()
    {
        // Debug.Log(IsGrounded());
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {

            if (IsGrounded())
            {
                jumps = 2;
            }

            if (IsLeftWalled())
            {
                Debug.Log("left jump " + (jumpingPower + 20));
                jumps = 2;
                rb.velocity = new Vector3(jumpingPower + 20f, jumpingPower, 0);
            }
            else if (IsRightWalled())
            {
                Debug.Log("right jump " + (jumpingPower + 20));
                jumps = 2;
                rb.velocity = new Vector3(-jumpingPower + 20f, jumpingPower, 0);
            }




            // if (jumps > 0 && (!IsLeftWalled() && !IsRightWalled()))
            // {
            //     Debug.Log("normal jump");
            //     rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            //     jumps--;
            // }
            Debug.Log(rb.velocity);

        }

        // if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        // }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VerticleSpeed", rb.velocity.y);
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
    }

    private bool IsLeftWalled()
    {
        return Physics2D.OverlapCircle(leftWallCheck.position, 0.2f, wallLayer);
    }
    private bool IsRightWalled()
    {
        return Physics2D.OverlapCircle(rightWallCheck.position, 0.2f, wallLayer);
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
