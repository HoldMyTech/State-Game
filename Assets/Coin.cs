using UnityEngine;


public class Coin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D Collision2D)
    {
        if (Collision2D.gameObject.tag == "Player")

        {
           
            Destroy(gameObject);
           

        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}