using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 25f;

    void Update()
    {
        // Note: "Input" must be capitalized
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
       
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; 

        
        Vector3 shootDirection = (mousePosition - transform.position).normalized;

        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        
        Destroy(bullet, 2f);
      
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * bulletSpeed;
        }
    }
}