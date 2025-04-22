using UnityEngine;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;
    Animator animator;
    BoxCollider2D bc;

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

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        rb2D.linearVelocity = new Vector2(moveInput * runSpeed, rb2D.linearVelocity.y);
    }
    void Update()
    {
        HandleInput();
        flipX();
        isMoving();
        Attack();
        Jump();
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
        }
    }
    void endAttack()
    {
        animator.SetBool("Attack1", false);
    }

    void Jump()
    {

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            rb2D.linearVelocity = Vector2.up * jumpForce;
            animator.SetBool("isJumping",true);
            jumpCooldown = 1f;
            canAttack = false;
        }

        if (jumpCooldown > 0)
        {
            jumpCooldown -= Time.deltaTime;
        }else if (IsGrounded() && animator.GetBool("isJumping"))
         {
            animator.SetBool("isJumping", false);
            canAttack = true;
         }
    }

}
