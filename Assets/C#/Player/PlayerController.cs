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
    BossHealth BossHealth;


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
    public bool isAttacking = false;

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
        BossHealth = GameObject.FindWithTag("Enemy").GetComponent<BossHealth>();
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
        if (moveInput < 0)
        {
            Vector3 pos = new Vector3(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
            transform.rotation = Quaternion.Euler(pos);
        }
        else if (moveInput > 0) 
        {
            Vector3 pos = new Vector3(transform.rotation.x, 0, transform.rotation.z);
            transform.rotation = Quaternion.Euler(pos);
        }
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
            isAttacking = true;
        }
    }

    public void AttackDamage()
    {
        Debug.Log("gotAttacked");
        BossHealth.BossGotHurt();
    }

    void endAttack()
    {
        animator.SetBool("Attack1", false);
        canDash = true;
        isAttacking = false;
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
        canAttack = false;
    }

    public void endHurt()
    {
        animator.SetBool("isHurt", false);
        canAttack = true;
    }
}
