using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int damage = 1;
    public int maxHealth = 3;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    private Color ogcolor; 
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        ogcolor = spriteRenderer.color; 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(FlashWhite()); 
        if (currentHealth <= 0)
        {
            Die(); 
        }
    }

    private IEnumerator FlashWhite()
    {
        spriteRenderer.color = Color.white; 
        yield return new WaitForSeconds(0.2f); 
        spriteRenderer.color = ogcolor;
    }
    void Die()
    {
        Destroy(gameObject); 
    }
}
