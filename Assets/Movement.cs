using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    
    public AudioClip SoundEffect;
    public AudioSource AS;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool isGrounded = true;

    private bool jumpPowerActive = false;
    private bool speedPowerActive = false;

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
        rb.freezeRotation = true;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Movement
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Flip sprite and set state
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
            State = PlayerState.Walking;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
            State = PlayerState.Walking;
        }
        else
        {
            State = PlayerState.Idle;
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            State = PlayerState.Jumping;
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

        if (collision.gameObject.CompareTag("Coin"))
        {
            if (AS && SoundEffect)
                AS.PlayOneShot(SoundEffect);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Debug.Log("PowerUp collected");
            Destroy(collision.gameObject);
            jumpForce = 12f;
            spriteRenderer.color = Color.green;
            jumpPowerActive = true;
            StartCoroutine(ResetJumpPower());
        }

        if (collision.gameObject.CompareTag("PowerUp2"))
        {
            Debug.Log("PowerUp2 collected");
            Destroy(collision.gameObject);
            moveSpeed = 12f;
            spriteRenderer.color = Color.yellow;
            speedPowerActive = true;
            StartCoroutine(ResetSpeedPower());
        }
    }

    private IEnumerator ResetJumpPower()
    {
        yield return new WaitForSeconds(5);
        jumpForce = 5f;
        jumpPowerActive = false;
        if (!speedPowerActive)
            spriteRenderer.color = Color.white;
    }

    private IEnumerator ResetSpeedPower()
    {
        yield return new WaitForSeconds(5);
        moveSpeed = 5f;
        speedPowerActive = false;
        if (!jumpPowerActive)
            spriteRenderer.color = Color.white;
    }
}
