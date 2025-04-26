using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;
    Animator animator;
    BoxCollider2D bc;
    TrailRenderer tr;


    [SerializeField] private LayerMask jumpLayer;
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, jumpLayer);
    }

    public float runSpeed = 6.0f;
    private float moveInput;

    public float jumpForce;
    private float jumpCooldown;

    private bool canAttack = true;

    private bool canDash = true;
    private bool DashOn;
    public float dashingPower;
    private float dashingTime = 0.07f;
    public float dashingCooldown = 1f;

    void Start()
    {
        tr = GetComponent<TrailRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (DashOn)
        {
            return;
        }
        rb2D.linearVelocity = new Vector2(moveInput * runSpeed, rb2D.linearVelocity.y);
    }
    void Update()
    {
        if (DashOn)
        {
            return;
        }
        HandleInput();
        flipX();
        Attack();
        Jump();
        isDashing();
        isMoving();
    }

    void HandleInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    void flipX()
    {
        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;
    }

    void isMoving()
    {
        animator.SetBool("isRunning", moveInput != 0);
    }

    void Attack()
    {

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            animator.SetBool("Attack1", true);
            canDash = false;
        }
    }
    void endAttack()
    {
        animator.SetBool("Attack1", false);
        canDash = true;
    }

    void Jump()
    {
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            rb2D.linearVelocity = Vector2.up * jumpForce;
            animator.SetBool("isJumping", true);
            jumpCooldown = 1f;
            canAttack = false;
        }

        if (jumpCooldown > 0)
        {
            jumpCooldown -= Time.deltaTime;
        }
        else if (IsGrounded() && animator.GetBool("isJumping"))
        {
            animator.SetBool("isJumping", false);
            canAttack = true;
        }
    }


    void isDashing()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        canDash = false;
        DashOn = true;
        float originalGravity = rb2D.gravityScale;
        rb2D.gravityScale = 0f;
        rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb2D.gravityScale = originalGravity;
        DashOn = false;
        Physics2D.IgnoreLayerCollision(6, 7, false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void isHurt()
    {
        animator.SetBool("isHurt", true);
    }

    public void endHurt()
    {
        Debug.Log("endHurt");
        animator.SetBool("isHurt", false);
    }
}
