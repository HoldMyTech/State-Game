using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneAdvance : MonoBehaviour
{
    public string nextSceneName;
    public AudioSource clickSound;

    public void LoadNextScene()
    {
        StartCoroutine(PlaySoundThenLoad());
    }

    private IEnumerator PlaySoundThenLoad()
    {
        if (clickSound != null && clickSound.clip != null)
        {
            clickSound.Play();
            yield return new WaitForSeconds(clickSound.clip.length);
        }

        SceneManager.LoadScene(nextSceneName);
    }
}