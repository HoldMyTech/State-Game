using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool isGrounded = true; // Simple ground check flag

    // Public fields for audio, though commented out in the original
    // public AudioClip SoundEffect;
    // public AudioSource AS;

    public PlayerState State;
    public enum PlayerState
    {
        None = 0,
        Idle = 1,
        Walking = 2,
        Jumping = 3,
        Stunned = 4
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        // Prevent unwanted rotations
        rb.freezeRotation = true;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Set horizontal velocity
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Flip sprite and set state based on horizontal input
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false; // Facing right
            State = PlayerState.Walking;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true; // Facing left
            State = PlayerState.Walking;
        }
        else // No horizontal input, player is idle
        {
            State = PlayerState.Idle; 
        }

        // Jump (only if grounded)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false; // Prevent double-jumping
            State = PlayerState.Jumping;
        }

      
        if (State == PlayerState.Walking)
        {
          
            spriteRenderer.color = Color.green; 
        }
        else if (State == PlayerState.Idle)
        {
            spriteRenderer.color = Color.red; 
        }
        else if (State == PlayerState.Jumping)
        {
            spriteRenderer.color = Color.cyan; 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}