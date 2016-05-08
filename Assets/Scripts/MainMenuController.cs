using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public AudioController audioController;

    void Update()
    {
		if (Input.anyKeyDown)
		{
            audioController.PlayButtonRestart();
            audioController.fadeMusicOut();
			StartCoroutine(OpenNextLevel());
		}
    }

    public void OnStartClicked()
    {
        audioController.PlayButtonRestart();
        audioController.fadeMusicOut();
        StartCoroutine(OpenNextLevel());
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

    private IEnumerator OpenNextLevel()
    {
        yield return new WaitForSeconds(1.1f);
        if (audioController.FadingOutEnded)
        {
            SceneManager.LoadScene("Play");
        }
        else
        {
            StartCoroutine(OpenNextLevel());
        }
    }
}
