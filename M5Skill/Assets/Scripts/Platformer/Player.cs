using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int coins;

    [SerializeField] int health = 4;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] int extraJumpValue = 1;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isGrounded;
    private int extraJump;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        extraJump = extraJumpValue;
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rigidbody.linearVelocity = new Vector2(moveInput * moveSpeed, rigidbody.linearVelocity.y);

        if (isGrounded)
            extraJump = extraJumpValue;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
                rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpForce);

            else if (extraJump > 0)
            {
                rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpForce);
                extraJump--;
            }
        }

        SetAnimation(moveInput);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (transform.position.y < -100f)
            Die();
    }

    private void SetAnimation(float moveInput)
    {
        if (isGrounded)
        {
            if (moveInput == 0)
                animator.Play("Player_Idle");

            else
                animator.Play("Player_Run");
        }
        else
        {
            if (rigidbody.linearVelocity.y > 0)
                animator.Play("Player_Jump");

            else
                animator.Play("Player_Fall");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            health -= 1;
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());

            if (health <= 0)
                Die();
        }
    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Platformer");
    }
}
