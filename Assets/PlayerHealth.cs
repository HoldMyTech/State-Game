using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; 
    private int currentHealth;
    
    public int damage = 1; 
    
    public Health HealthUI; // Renamed for clarity, assuming this refers to a UI script
    
    void Start()
    {
        currentHealth = maxHealth;
        if (HealthUI != null)
        {
            HealthUI.SetMaxHearts(maxHealth);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision) // Corrected method name
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (collision.gameObject.tag.Equals("Enemy")) 
        {
            TakeDamage(1); 
        }
        
        if (collision.gameObject.tag.Equals("End")) 
        {
            SceneManager.LoadScene("Win");
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
            SceneManager.LoadScene("Lose");
        }
    }
    
    void Update()
    {
        // Press 'H' to simulate taking 1 damage
        //if (Input.GetKeyDown(KeyCode.H))
        {
            //TakeDamage(1);
        }
    }

}