using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    
    public float chaseSpeed = 2f;
    public float chaseRange = 5f;  // Distance at which enemy starts chasing
    public LayerMask groundLayer;
    public float groundCheckDistance = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Auto-find the player by tag if not assigned
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player not found in scene!");
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        // Check if enemy is on the ground
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        // Calculate distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Only chase if within range
        if (isGrounded && distanceToPlayer <= chaseRange)
        {
            float direction = Mathf.Sign(player.position.x - transform.position.x);

            // Move toward the player
            rb.linearVelocity = new Vector2(direction * chaseSpeed, rb.linearVelocity.y);

            // Flip sprite
            if (direction < 0)
                spriteRenderer.flipX = false;
            else if (direction > 0)
                spriteRenderer.flipX = true;
        }
        else
        {
            // Stop moving if player is out of range or enemy not grounded
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    // Visualize chase range & ground check in editor
    void OnDrawGizmosSelected()
    {
        // Draw chase range circle
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // Draw ground check ray
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
