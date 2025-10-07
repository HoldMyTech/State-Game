using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; 
    private int currentHealth;
    
    public Health HealthUI; // Renamed for clarity, assuming this refers to a UI script
    
    void Start()
    {
        currentHealth = maxHealth;
        if (HealthUI != null)
        {
            HealthUI.SetMaxHearts(maxHealth);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision) // Corrected method name
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null) 
        {
            
            TakeDamage(enemy.damage); 
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (HealthUI != null)
        {
            HealthUI.UpdateHearts(currentHealth); 
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Player Defeated!"); // Example: Add end screen logic here
            // e.g., SceneManager.LoadScene("GameOverScene");
        }
    }
    
    void Update()
    {
        // Press 'H' to simulate taking 1 damage
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1);
        }
    }

}