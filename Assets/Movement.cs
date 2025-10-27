using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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

    //Movement 
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

      //Potentially used for animation later 
        if (State == PlayerState.Walking)
        {
          
            //spriteRenderer.color = Color.green; 
        }
        else if (State == PlayerState.Idle)
        {
            //spriteRenderer.color = Color.red; 
        }
        else if (State == PlayerState.Jumping)
        {
            //spriteRenderer.color = Color.cyan; 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("FallDamage"))
        {
            SceneManager.LoadScene("Lose");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Debug.Log("PowerUp");
            Destroy(collision.gameObject);
            jumpForce = 10;
            spriteRenderer.color = Color.green;
            StartCoroutine(ResetPower());
        }

        if (collision.gameObject.CompareTag("PowerUp2"))
        {
            Debug.Log("PowerUp2");
            Destroy(collision.gameObject);
            moveSpeed = 10;
            spriteRenderer.color = Color.yellow;
            StartCoroutine(ResetPower());
        }
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(5);
        jumpForce = 5;
        moveSpeed = 5;
        spriteRenderer.color = Color.white;
    }
}