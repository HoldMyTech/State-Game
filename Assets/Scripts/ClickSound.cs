using UnityEngine;

public class UISound : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayClickSound()
    {
        Debug.Log("Click sound triggered");
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}