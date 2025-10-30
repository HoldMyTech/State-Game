using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // Required for List

public class Health : MonoBehaviour
{
    public Image heartPrefab;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private List<Image> hearts = new List<Image>();

    public void SetMaxHearts(int maxHearts) // Changed parameter name to lowercase 'm' for consistency
    {
        foreach (Image heart in hearts)
        {
            Destroy(heart.gameObject);
        }
        hearts.Clear();

        for (int i = 0; i < maxHearts; i++)
        {
            Image newHeart = Instantiate(heartPrefab, transform);
            newHeart.sprite = fullHeart;
            hearts.Add(newHeart);
        }
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}